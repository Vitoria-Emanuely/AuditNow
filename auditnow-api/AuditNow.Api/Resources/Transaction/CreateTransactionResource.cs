#region Using
using AuditNow.Core.Models.Enums;
#endregion

namespace AuditNow.Api.Resources.Transaction
{
    public class CreateTransactionResource
    {

        public TransactionType TransactionType { get; set; }

        public double Value { get; set; }

    }
}