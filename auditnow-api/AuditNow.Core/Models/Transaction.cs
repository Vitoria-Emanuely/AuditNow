#region Using
using AuditNow.Core.Models.Enums;
#endregion

namespace AuditNow.Core.Models
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }

        public TransactionType TransactionType { get; set; }

        public double Value { get; set; }

        public double Balance { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
