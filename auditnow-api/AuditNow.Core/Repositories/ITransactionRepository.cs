#region Using
using AuditNow.Core.Models;
#endregion

namespace AuditNow.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {

        IEnumerable<Transaction> GetTransactionsByFilter(Transaction transaction, bool? isActive, int? requestUserId);

        Transaction GetTransactionById(int transactionId, bool? isActive);


    }
}