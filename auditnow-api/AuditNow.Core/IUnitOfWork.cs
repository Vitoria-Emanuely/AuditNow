#region Using
using AuditNow.Core.Repositories;
#endregion

namespace AuditNow.Core
{
    public interface IUnitOfWork : IDisposable
    {

        ITransactionRepository TbTransaction { get; }

        IUserRepository TbUser { get; }


        Task<int> CommitAsync();

    }
}