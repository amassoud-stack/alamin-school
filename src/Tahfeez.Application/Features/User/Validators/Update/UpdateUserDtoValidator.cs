using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.User.DTOs;

namespace Tahfeez.Application.Features.User.Validators.Update
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(u => u.UserName).MinimumLength(3);
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Role).IsInEnum();
        }
    }
}
