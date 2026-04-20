using MediatR;
using Tahfeez.Application.Features.Attendance.DTOs.Attendance;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Queries.GetAttendanceReport;

public record GetAttendanceReportQuery(Guid ClassId, DateOnly From, DateOnly To)
    : IRequest<Result<IEnumerable<AttendanceDto>>>;