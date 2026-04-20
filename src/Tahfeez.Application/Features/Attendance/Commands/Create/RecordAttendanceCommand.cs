using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Attendance.DTOs.Create;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Commands.Create;

public record RecordAttendanceCommand(
    RecordAttendanceDto recordAttendance
) : IRequest<Result<Guid>>;
