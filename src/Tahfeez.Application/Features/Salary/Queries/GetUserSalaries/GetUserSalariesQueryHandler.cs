using MediatR;
using Tahfeez.Application.Features.Salary.DTOs;
using Tahfeez.Application.Features.Salary.Mappings;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Salary.Queries.GetUserSalaries;

public class GetUserSalariesHandler : IRequestHandler<GetUserSalariesQuery, Result<IEnumerable<SalaryDto>>>
{
    private readonly ISalaryRepository _repo;

    public GetUserSalariesHandler(ISalaryRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<SalaryDto>>> Handle(GetUserSalariesQuery request, CancellationToken cancellationToken)
    {
        var salaries = await _repo.GetByUserAsync(request.UserId, cancellationToken);
        return Result.Success(salaries.Select(s => s.Map()));
    }
}
