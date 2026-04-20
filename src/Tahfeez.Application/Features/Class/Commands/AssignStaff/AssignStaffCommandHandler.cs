using MediatR;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Commands.AssignStaff;

public class AssignStaffCommandHandler : IRequestHandler<AssignStaffCommand, Result>
{
    private readonly IClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public AssignStaffCommandHandler(IClassRepository classRepo, IUnitOfWork uow)
    {
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Result> Handle(AssignStaffCommand request, CancellationToken cancellationToken)
    {
        var entity = await _classRepo.GetByIdAsync(request.ClassId, cancellationToken);
        if (entity is null)
            return Result.Failure($"Class '{request.ClassId}' not found.");

        entity.TeacherId = request.Dto.TeacherId;
        entity.AssistantId = request.Dto.AssistantId;
        entity.SupervisorId = request.Dto.SupervisorId;

        await _classRepo.UpdateAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
