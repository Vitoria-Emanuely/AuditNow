#region Using
using AuditNow.Api.Resources.User;
using AuditNow.Core.Models.Enums;
#endregion

namespace AuditNow.Api.Resources.Transaction
{
    public class TransactionResource
    {
        public int TransactionId { get; set; }

        public TransactionType TransactionType { get; set; }

        public double Value { get; set; }

        public double Balance { get; set; }

        public int UserId { get; set; }

        public UserResource User { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
