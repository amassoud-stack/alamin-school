using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Role.DTOs
{
    public record AssignRoleDto
        (
            UserRole Role
        );
}
