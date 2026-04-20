using MediatR;
using Tahfeez.Application.Features.Attendance.DTOs.Attendance;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Queries.GetAttendanceByUser;

public record GetAttendanceByUserQuery(Guid UserId)
    : IRequest<Result<IEnumerable<AttendanceDto>>>;
