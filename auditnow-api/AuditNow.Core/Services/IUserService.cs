#region Using
using AuditNow.Core.Models;
using AuditNow.Core.Models.ValueObjects;
#endregion

namespace AuditNow.Core.Services
{
    public interface IUserService
    {

        ReturnObject<User> GetUserById(int userId, bool? isActive, int? requestUserCompanyId);

    }
}