using MediatR;
using Tahfeez.Domain.Enums;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Salary.Commands.CreateSalary;

public class CreateSalaryHandler : IRequestHandler<CreateSalaryCommand, Result<Guid>>
{
    private readonly ISalaryRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateSalaryHandler(ISalaryRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Salaries.Salary
        {
            UserId = request.UserId,
            Role = request.Role,
            Amount = request.Amount,
            SalaryMonth = request.SalaryMonth,
            Status = SalaryStatus.Unpaid,
            Notes = request.Notes
        };

        await _repo.AddAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success(entity.Id);
    }
}
