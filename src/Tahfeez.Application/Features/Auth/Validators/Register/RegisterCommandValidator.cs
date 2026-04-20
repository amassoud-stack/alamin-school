using FluentValidation;
using System.Text.RegularExpressions;
using Tahfeez.Application.Features.Auth.Commands.Register;
using Tahfeez.Application.Utilities;
using Tahfeez.Domain.Entities.Roles;

namespace Tahfeez.Application.Features.Auth.Validators.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(c => c.Email).EmailAddress().NotEmpty().NotNull();
            RuleFor(c => c.UserName).MinimumLength(3).NotEmpty().NotNull();
            RuleFor(c => c.Password).Must(ValidatorsUtilities.PasswordValidator).NotEmpty().NotNull();
            RuleFor(c => c.ConfirmPassword).Equal(c=>c.Password);
            RuleFor(c => c.Role).NotEmpty().NotNull();
        }
    }
}
