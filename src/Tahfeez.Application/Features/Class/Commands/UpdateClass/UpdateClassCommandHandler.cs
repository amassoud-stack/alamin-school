using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Commands.UpdateClass;

public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, Result>
{
    private readonly IClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public UpdateClassCommandHandler(IClassRepository classRepo, IUnitOfWork uow)
    {
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Result> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
    {
        var entity = await _classRepo.GetByIdAsync(request.ClassId, cancellationToken);
        if (entity is null)
            return Result.Failure($"Class '{request.ClassId}' not found.");

        entity.Name = request.Dto.Name;
        entity.Type = request.Dto.Type;
        entity.Mode = request.Dto.Mode;

        await _classRepo.UpdateAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}