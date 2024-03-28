#region Using
using AuditNow.Core.Models;
using AuditNow.Data.Configuration;
using Microsoft.EntityFrameworkCore;
#endregion

namespace AuditNow.Data
{
    public class AuditNowDbContext : DbContext
    {

        public DbSet<Transaction> TbTransaction { get; set; }

        public DbSet<User> TbUser{ get; set; }


        public AuditNowDbContext(DbContextOptions<AuditNowDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TransactionConfiguration());

        }


    }
}