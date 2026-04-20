using FluentValidation;
using Tahfeez.Application.Features.Recitation.Commands.LogRecitation;

namespace Tahfeez.Application.Features.Recitation.Validators;

public class LogRecitationCommandValidator : AbstractValidator<LogRecitationCommand>
{
    public LogRecitationCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required.");

        RuleFor(x => x.TeacherId)
            .NotEmpty().WithMessage("Teacher ID is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Recitation date is required.")
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Recitation date cannot be in the future.");

        RuleFor(x => x.AyahsCount)
            .GreaterThan(0).WithMessage("Ayahs count must be greater than zero.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid recitation type.");

        RuleFor(x => x.Grade)
            .InclusiveBetween(1, 10).WithMessage("Grade must be between 1 and 10.");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.")
            .When(x => x.Notes != null);
    }
}
