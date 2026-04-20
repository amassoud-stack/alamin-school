using MediatR;
using Tahfeez.Application.Features.Attendance.DTOs.Update;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Commands.Update;
public record UpdateAttendanceCommand(
    Guid id,
    UpdateAttendanceDto updateAttendance
) : IRequest<Result>;

