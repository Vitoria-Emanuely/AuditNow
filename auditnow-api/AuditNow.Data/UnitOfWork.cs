#region Using
using AuditNow.Core;
using AuditNow.Core.Repositories;
using AuditNow.Data.Repositories;
#endregion


namespace AuditNow.Data
{
    public class UnitOfWork : IUnitOfWork
    {


        private readonly AuditNowDbContext _context;
        private TransactionRepository _transactionRepository;
        private UserRepository _userRepository;


        public UnitOfWork(AuditNowDbContext context)
        {
            this._context = context;
        }


        public ITransactionRepository TbTransaction => _transactionRepository = _transactionRepository ?? new TransactionRepository(_context);

        public IUserRepository TbUser => _userRepository = _userRepository ?? new UserRepository(_context);


        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }


    }
}