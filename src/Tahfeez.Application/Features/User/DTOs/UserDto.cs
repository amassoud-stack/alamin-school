using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.User.DTOs;

public record UserDto(
    Guid Id,
    string FullName,
    string? Email,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    UserStatus Status
);
