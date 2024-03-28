#region Using
using AuditNow.Api.Resources.User;
using FluentValidation;
#endregion

namespace AuditNow.Api.Validators.User
{
    public class CreateUserResourceValidator : AbstractValidator<CreateUserResource>
    {
        public CreateUserResourceValidator()
        {

            RuleFor(a => a.UserName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(a => a.Email)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(a => a.Password)
                .NotEmpty()
                .MaximumLength(50);

        }
    }
}