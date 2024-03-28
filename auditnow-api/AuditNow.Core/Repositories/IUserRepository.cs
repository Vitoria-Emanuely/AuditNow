#region Using
using AuditNow.Core.Models;
#endregion

namespace AuditNow.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(int userId, int? companyId, bool? isActive);

    }
}