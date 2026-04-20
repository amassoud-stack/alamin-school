using MediatR;
using Tahfeez.Application.Features.Salary.DTOs;
using Tahfeez.Application.Features.Salary.Mappings;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Salary.Queries.GetSalariesByMonth;

public class GetSalariesByMonthHandler : IRequestHandler<GetSalariesByMonthQuery, Result<IEnumerable<SalaryDto>>>
{
    private readonly ISalaryRepository _repo;

    public GetSalariesByMonthHandler(ISalaryRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<SalaryDto>>> Handle(GetSalariesByMonthQuery request, CancellationToken cancellationToken)
    {
        var salaries = await _repo.GetByMonthAsync(request.Month, cancellationToken);
        return Result.Success(salaries.Select(s => s.Map()));
    }
}
