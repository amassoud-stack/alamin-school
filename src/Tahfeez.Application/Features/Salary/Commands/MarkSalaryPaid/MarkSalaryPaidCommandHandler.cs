using MediatR;
using Tahfeez.Domain.Enums;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Salary.Commands.MarkSalaryPaid;

public class MarkSalaryPaidHandler : IRequestHandler<MarkSalaryPaidCommand, Result>
{
    private readonly ISalaryRepository _repo;
    private readonly IUnitOfWork _uow;

    public MarkSalaryPaidHandler(ISalaryRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result> Handle(MarkSalaryPaidCommand request, CancellationToken cancellationToken)
    {
        var salary = await _repo.GetByIdAsync(request.SalaryId, cancellationToken);
        if (salary is null) return Result.Failure("Salary record not found.");

        salary.Status = SalaryStatus.Paid;
        salary.PaidAt = DateTime.UtcNow;

        await _repo.UpdateAsync(salary, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
