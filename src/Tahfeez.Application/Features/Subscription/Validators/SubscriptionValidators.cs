using FluentValidation;
using Tahfeez.Application.Features.Subscription.Commands.CreateSubscription;

namespace Tahfeez.Application.Features.Subscription.Validators;

public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Subscription amount must be greater than zero.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid subscription type.");

        RuleFor(x => x.Mode)
            .IsInEnum().WithMessage("Invalid subscription mode.");

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid payment method.");

        RuleFor(x => x.PaymentDate)
            .NotEmpty().WithMessage("Payment date is required.");
    }
}
