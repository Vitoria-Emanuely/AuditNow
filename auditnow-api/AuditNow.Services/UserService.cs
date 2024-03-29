#region Using
using AuditNow.Core;
using AuditNow.Core.Models.ValueObjects;
using AuditNow.Core.Models;
using AuditNow.Core.Services;
using AuditNow.Core.Common;
using Microsoft.Extensions.Configuration;
#endregion

namespace AuditNow.Services
{
    public class UserService : IUserService
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;


        public UserService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            this._unitOfWork = unitOfWork;
            _config = config;
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

            newUser.Password = new Cryptography().Encrypt(newUser.Password);

            await _unitOfWork.TbUser.AddAsync(newUser);
            await _unitOfWork.CommitAsync();

            ret.IsSuccessful = true;
            ret.Message = "Usuário criado com sucesso";
            ret.Data = new List<User> { newUser };

            return ret;
        }


        public async Task<ReturnObject<User>> Login(string email, string password)
        {
            ReturnObject<User> ret = new ReturnObject<User>();

            string encryptedPassword = new Cryptography().Encrypt(password);

            User user = _unitOfWork.TbUser.GetUserByLogin(email, encryptedPassword);
            if (user == null)
            {
                ret.IsSuccessful = false;
                ret.Message = "Dados inválidos do usuário";
                return ret;
            }

            user.JwtToken = new TokenManager().GetToken(user, _config);

            await UpdateUserLastAccess(user, DateTime.UtcNow);

            ret.IsSuccessful = true;
            ret.Data = new List<User> { user };

            return ret;
        }


        private async Task UpdateUserLastAccess(User userToBeUpdated, DateTime lastAccessDate)
        {
            userToBeUpdated.LastAccessDate = lastAccessDate;

            await _unitOfWork.CommitAsync();
        }

    }
}