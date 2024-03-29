#region Using
using AuditNow.Api.Resources.Transaction;
using FluentValidation;
#endregion

namespace AuditNow.Api.Validators.Transaction
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionResource>
    {
        public CreateTransactionValidator()
        {
            RuleFor(a => a.TransactionType)
                .NotEmpty();

            RuleFor(a => a.Value)
                .NotEmpty();
        }
    }
}
