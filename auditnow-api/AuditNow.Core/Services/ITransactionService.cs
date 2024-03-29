#region Using
using AuditNow.Core.Models;
using AuditNow.Core.Models.ValueObjects;
#endregion

namespace AuditNow.Core.Services
{
    public interface ITransactionService
    {

        Task<ReturnObject<Transaction>> CreateTransaction(Transaction newTransaction);

        ReturnObject<Transaction> GetTransactionByFilter(Transaction transaction, bool? isActive, int? requestUserId);

        ReturnObject<Transaction> GetTransactionById(int transactionId, bool? isActive);

    }
}
