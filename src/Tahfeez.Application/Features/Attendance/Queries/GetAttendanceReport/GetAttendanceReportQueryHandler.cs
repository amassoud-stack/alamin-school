using Mapster;
using MediatR;
using Tahfeez.Application.Features.Attendance.DTOs.Attendance;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Queries.GetAttendanceReport;

public class GetAttendanceReportQueryHandler : IRequestHandler<GetAttendanceReportQuery, Result<IEnumerable<AttendanceDto>>>
{
    private readonly IAttendanceRepository _repo;
    public GetAttendanceReportQueryHandler(IAttendanceRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<AttendanceDto>>> Handle(GetAttendanceReportQuery request, CancellationToken cancellationToken)
    {
        var records = await _repo.GetByClassAndDateRangeAsync(request.ClassId, request.From, request.To, cancellationToken);
        return Result.Success(records.Adapt<IEnumerable<AttendanceDto>>());
    }
}
