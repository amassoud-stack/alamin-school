using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Attendance.DTOs.Create;

public record RecordAttendanceDto(
    Guid UserId,
    DateOnly Date,
    AttendanceStatus Status,
    string? Notes
    );

