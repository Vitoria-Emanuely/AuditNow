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

        public ReturnObject<User> GetUserById(int userId, bool? isActive)
        {
            ReturnObject<User> ret = new ReturnObject<User>();

            User user = _unitOfWork.TbUser.GetUserById(userId, isActive);
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


        public async Task<ReturnObject<User>> CreateUser(User newUser)
        {
            ReturnObject<User> ret = new ReturnObject<User>();

            await _unitOfWork.TbUser.AddAsync(newUser);
            await _unitOfWork.CommitAsync();

            ret.IsSuccessful = true;
            ret.Message = "Usuário criado com sucesso";
            ret.Data = new List<User> { newUser };

            return ret;
        }

    }
}