using FluentValidation;
using Tahfeez.Application.Features.Salary.Commands.CreateSalary;

namespace Tahfeez.Application.Features.Salary.Validators;

public class CreateSalaryCommandValidator : AbstractValidator<CreateSalaryCommand>
{
    public CreateSalaryCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Salary amount must be greater than zero.");

        RuleFor(x => x.SalaryMonth)
            .NotEmpty().WithMessage("Salary month is required.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
            .When(x => x.Notes != null);
    }
}
