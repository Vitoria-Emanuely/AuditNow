#region Using
using AuditNow.Core;
using AuditNow.Core.Models;
using AuditNow.Core.Models.Enums;
using AuditNow.Core.Models.Support;
using AuditNow.Core.Models.ValueObjects;
using AuditNow.Core.Services;
#endregion

namespace AuditNow.Services
{
     public class TransactionService : ITransactionService
    {


        private readonly IUnitOfWork _unitOfWork;


        public TransactionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public ReturnObject<Transaction> GetTransactionByFilter(Transaction transaction, bool? isActive, int? requestUserId)
        {
            ReturnObject<Transaction> ret = new ReturnObject<Transaction>();

            List<Transaction> transactions = _unitOfWork.TbTransaction.GetTransactionsByFilter(transaction, isActive, requestUserId).ToList();
            if (transactions.Count == 0)
            {
                ret.IsSuccessful = false;
                ret.Message = "Não há resultados encontrados.";
                return ret;
            }

            ret.IsSuccessful = true;
            ret.Data = transactions;

            return ret;
        }


        public async Task<ReturnObject<Transaction>> CreateTransaction(Transaction newTransaction)
        {
            ReturnObject<Transaction> ret = new ReturnObject<Transaction>();

            if (newTransaction.TransactionType == TransactionType.Deposit) 
            {
                newTransaction.Balance += newTransaction.Value;
            }

            if (newTransaction.TransactionType == TransactionType.Purchase || newTransaction.TransactionType == TransactionType.Withdrawal)
            {
                TransactionCheck lastTransaction = CheckBalance(newTransaction);

                if (!lastTransaction.HasBalance)
                {
                    ret.IsSuccessful = false;
                    ret.Message = "Não há saldo suficiente";
                    return ret;
                }

                newTransaction.Balance = lastTransaction.Transaction.Balance - newTransaction.Value;
            }

            await _unitOfWork.TbTransaction.AddAsync(newTransaction);
            await _unitOfWork.CommitAsync();

            ret.IsSuccessful = true;
            ret.Message = "Transação realizada com sucesso";
            ret.Data = new List<Transaction> { newTransaction };

            return ret;
        }


        public ReturnObject<Transaction> GetTransactionById(int transactionId, bool? isActive)
        {
            ReturnObject<Transaction> ret = new ReturnObject<Transaction>();

            Transaction transaction = _unitOfWork.TbTransaction.GetTransactionById(transactionId, isActive);
            if (transaction == null)
            {
                ret.IsSuccessful = false;
                ret.Message = "Transação não encontrada";
                return ret;
            }

            ret.IsSuccessful = true;
            ret.Data = new List<Transaction> { transaction };

            return ret;
        }


        private TransactionCheck CheckBalance(Transaction transaction)
        {
            TransactionCheck ret = new TransactionCheck
            {
                Transaction = transaction,
                HasBalance = true
            };

            transaction.TransactionType = 0;
            List<Transaction> transactions = _unitOfWork.TbTransaction.GetTransactionsByFilter(transaction, true, transaction.UserId).ToList();

            if (transactions.Count == 0) {
                ret.HasBalance = false;
            }

            if (transactions.Count > 0)
            {
                if (transactions.LastOrDefault().Balance < transaction.Value)
                {
                    ret.HasBalance = false;         }
            }

            ret.Transaction = transactions.LastOrDefault();

            return ret;
        }

    }
}
