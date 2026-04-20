using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Entities.Roles;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand
    (
        string UserName,
        string Password,
        string ConfirmPassword,
        string Email,
        UserRole Role
    ) : IRequest<Result>;
    
}
