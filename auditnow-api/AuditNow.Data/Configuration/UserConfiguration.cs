#region Using
using AuditNow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

namespace AuditNow.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(a => a.UserId);

            builder
                .Property(m => m.UserId)
                .UseMySqlIdentityColumn();

            builder
                .Property(m => m.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.Password)
                .HasMaxLength(100);

            builder
                .Property(m => m.CreationUserId)
                .IsRequired();

            builder
                .Property(m => m.CreationDate)
                .IsRequired();

            builder
                .Property(m => m.ModificationUserId)
                .IsRequired();

            builder
                .Property(m => m.ModificationDate)
                .IsRequired();

            builder
                .Property(m => m.IsActive)
                .IsRequired();

            builder
                .ToTable("TbUser");
        }

    }
}