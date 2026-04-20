using Tahfeez.Domain.Entities.Roles;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.User.DTOs
{
    public record UpdateUserDto
    (
        string? UserName,
        string? Email,
        bool? IsActive,
        UserRole? Role
    );
}
