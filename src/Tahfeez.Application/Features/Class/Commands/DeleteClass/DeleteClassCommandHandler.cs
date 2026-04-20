using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Commands.DeleteClass;


public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand, Result>
{
    private readonly IClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public DeleteClassCommandHandler(IClassRepository classRepo, IUnitOfWork uow)
    {
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Result> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
    {
        var entity = await _classRepo.GetByIdAsync(request.ClassId, cancellationToken);
        if (entity is null)
            return Result.Failure($"Class '{request.ClassId}' not found.");

        entity.IsDeleted = true;

        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}