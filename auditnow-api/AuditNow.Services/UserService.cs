#region Using
using AuditNow.Core;
using AuditNow.Core.Models.ValueObjects;
using AuditNow.Core.Models;
using AuditNow.Core.Services;
#endregion

namespace AuditNow.Services
{
    public class UserService : IUserService
    {


        private readonly IUnitOfWork _unitOfWork;


        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ReturnObject<User> GetUserById(int userId, bool? isActive, int? requestUserCompanyId)
        {
            ReturnObject<User> ret = new ReturnObject<User>();

            User user = _unitOfWork.TbUser.GetUserById(userId, requestUserCompanyId, isActive);
            if (user == null)
            {
                ret.IsSuccessful = false;
                ret.Message = "Usuário não encontrado";
                return ret;
            }

            ret.IsSuccessful = true;
            ret.Data = new List<User> { user };

            return ret;
        }

    }
}