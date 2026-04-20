using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Attendance.DTOs.Attendance;

public record AttendanceDto(
    Guid Id,
    Guid UserId,
    string UserName,
    DateOnly Date,
    AttendanceStatus Status,
    string? Notes
);