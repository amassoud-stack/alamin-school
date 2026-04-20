using FluentValidation;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.Application.Utilities;

namespace Tahfeez.Application.Features.User.Validators.Create
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Must(ValidatorsUtilities.PasswordValidator).WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and be at least 8 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .IsInEnum();
        }
    }
}
