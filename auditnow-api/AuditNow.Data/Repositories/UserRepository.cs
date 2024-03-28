#region Using
using AuditNow.Core.Models;
using AuditNow.Core.Repositories;
using Microsoft.EntityFrameworkCore;
#endregion

namespace AuditNow.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {


        public UserRepository(AuditNowDbContext context) : base(context)
        { 

        }

        private AuditNowDbContext AuditNowDbContext
        {
            get { return Context as AuditNowDbContext; }
        }

        public User GetUserById(int userId, bool? isActive)
        {
            IQueryable<User> query = AuditNowDbContext.TbUser;

            query = query.Where(p => p.UserId == userId);

            if (isActive != null)
                query = query.Where(p => p.IsActive == isActive);

            return query.FirstOrDefault();
        }

    }
}