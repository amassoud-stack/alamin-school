using MediatR;
using Tahfeez.Application.Features.Attendance.DTOs.Attendance;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Queries.GetAttendanceByDate;

public record GetAttendanceByDateQuery(DateOnly Date)
    : IRequest<Result<IEnumerable<AttendanceDto>>>;