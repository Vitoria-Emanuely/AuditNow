#region Using
using AuditNow.Core.Models;
using AuditNow.Core.Repositories;
#endregion

namespace AuditNow.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {


        public TransactionRepository(AuditNowDbContext context) : base(context)
        {

        }


        private AuditNowDbContext AuditNowDbContext
        {
            get { return Context as AuditNowDbContext; }
        }


        public Transaction GetTransactionById(int transactionId, bool? isActive)
        {
            IQueryable<Transaction> query = AuditNowDbContext.TbTransaction;

            query = query.Where(p => p.TransactionId == transactionId);

            if (isActive != null)
                query = query.Where(p => p.IsActive == isActive);

            return query.FirstOrDefault();
        }


        public IEnumerable<Transaction> GetTransactionsByFilter(Transaction transaction, bool? isActive, int? requestUserId)
        {
            IQueryable<Transaction> query = AuditNowDbContext.TbTransaction;

            query = query.Where(p => p.UserId == transaction.UserId);

            if (transaction.TransactionType > 0)
                query = query.Where(p => p.TransactionType == transaction.TransactionType);

            if (requestUserId != null)
                query = query.Where(p => p.UserId == requestUserId);

            //if (!string.IsNullOrEmpty(transaction.CreationDate))
            //    query = query.Where(p => p.TransactionName.Contains(transaction.TransactionName));

            if (isActive != null)
                query = query.Where(p => p.IsActive == isActive);

            return query.OrderByDescending(a => a.TransactionId).ToList();
        }
    }
}
