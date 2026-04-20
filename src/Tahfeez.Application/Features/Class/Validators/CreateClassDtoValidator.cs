using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Class.DTOs;

namespace Tahfeez.Application.Features.Class.Validators;

public class CreateClassDtoValidator : AbstractValidator<CreateClassDto>
{
    public CreateClassDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Class name is required.")
            .MaximumLength(200).WithMessage("Class name must not exceed 200 characters.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid class type.");

        RuleFor(x => x.Mode)
            .IsInEnum().WithMessage("Invalid class mode.");
    }
}