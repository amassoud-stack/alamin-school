using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Attendance.DTOs.Create;

namespace Tahfeez.Application.Features.Attendance.Validators;

public class RecordAttendanceCommandValidator : AbstractValidator<RecordAttendanceDto>
{
    public RecordAttendanceCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Attendance date is required.")
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Attendance date cannot be in the future.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid attendance status.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
            .When(x => x.Notes != null);
    }
}
