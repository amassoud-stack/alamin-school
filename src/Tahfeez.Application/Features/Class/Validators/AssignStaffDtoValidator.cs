using FluentValidation;
using Tahfeez.Application.Features.Class.DTOs;

namespace Tahfeez.Application.Features.Class.Validators;


public class AssignStaffDtoValidator : AbstractValidator<AssignStaffDto>
{
    public AssignStaffDtoValidator()
    {
        // At least one staff member must be assigned
        RuleFor(x => x)
            .Must(x => x.TeacherId.HasValue || x.AssistantId.HasValue || x.SupervisorId.HasValue)
            .WithMessage("At least one staff member (Teacher, Assistant, or Supervisor) must be assigned.");
    }
}
