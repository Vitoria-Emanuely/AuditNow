using AuditNow.Core.Models.Enums;

namespace AuditNow.Core.Models
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }

        public TransactionType TransactionType { get; set; }

        public string Value { get; set; }

        public string Balance { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}
