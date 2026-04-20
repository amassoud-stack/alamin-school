using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Attendance.DTOs.Attendance;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Queries.GetAttendanceByDate;

public class GetAttendanceByDateQueryHandler : IRequestHandler<GetAttendanceByDateQuery, Result<IEnumerable<AttendanceDto>>>
{
    private readonly IAttendanceRepository _repo;
    public GetAttendanceByDateQueryHandler(IAttendanceRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<AttendanceDto>>> Handle(GetAttendanceByDateQuery request, CancellationToken cancellationToken)
    {
        var records = await _repo.GetByDateAsync(request.Date, cancellationToken);

        return Result.Success(records.Adapt<IEnumerable<AttendanceDto>>());
    }
}
