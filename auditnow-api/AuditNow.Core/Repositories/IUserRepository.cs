    #region Using
using AuditNow.Core.Models;
#endregion

namespace AuditNow.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(int userId, bool? isActive);

        User GetUserByLogin(string email, string password);

    }
}