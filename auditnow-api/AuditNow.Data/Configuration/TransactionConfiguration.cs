#region Using
using AuditNow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

namespace AuditNow.Data.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {

        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
                .HasKey(a => a.TransactionId);

            builder
                .Property(m => m.TransactionId)
                .UseMySqlIdentityColumn();

            builder
                .Property(m => m.TransactionType)
                .IsRequired();

            builder
                .Property(m => m.Value)
                .IsRequired();

            builder
                .Property(m => m.Balance)
                .IsRequired();

            builder
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

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
                .ToTable("TbTransaction");
        }

    }
}