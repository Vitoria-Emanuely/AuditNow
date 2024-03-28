#region Using
using AuditNow.Core;
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


    }
}
