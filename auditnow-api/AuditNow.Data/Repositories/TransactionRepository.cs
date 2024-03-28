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
    }
}
