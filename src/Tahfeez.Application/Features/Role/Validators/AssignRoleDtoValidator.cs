using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Role.DTOs;

namespace Tahfeez.Application.Features.Role.Validators
{
    public class AssignRoleDtoValidator :AbstractValidator<AssignRoleDto>
    {
        public AssignRoleDtoValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .IsInEnum();
        }
    }
}
