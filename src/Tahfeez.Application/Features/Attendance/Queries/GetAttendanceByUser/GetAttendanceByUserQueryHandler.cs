using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Attendance.DTOs.Attendance;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Queries.GetAttendanceByUser;

public class GetAttendanceByUserQueryHandler : IRequestHandler<GetAttendanceByUserQuery, Result<IEnumerable<AttendanceDto>>>
{
    private readonly IAttendanceRepository _repo;
    public GetAttendanceByUserQueryHandler(IAttendanceRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<AttendanceDto>>> Handle(GetAttendanceByUserQuery request, CancellationToken cancellationToken)
    {
        var records = await _repo.GetByUserAsync(request.UserId, cancellationToken);
        return Result.Success(records.Adapt<IEnumerable<AttendanceDto>>());
    }
}

