using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.User.DTOs
{
    public record CreateUserDto
    (
        string UserName,
        string Email,
        string Password,
        UserRole Role,
        bool IsActive
    );
}