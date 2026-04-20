using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Attendance.DTOs.Update;
public record UpdateAttendanceDto(AttendanceStatus Status, string? Notes);
