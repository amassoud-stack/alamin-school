using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Commands.CreateClass;

public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Result<Guid>>
{
    private readonly IClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public CreateClassCommandHandler(IClassRepository classRepo, IUnitOfWork uow)
    {
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var entity = new Domain.Entities.Classes.Class
        {
            Name = dto.Name,
            Type = dto.Type,
            Mode = dto.Mode,
            TeacherId = dto.TeacherId,
            AssistantId = dto.AssistantId,
            SupervisorId = dto.SupervisorId
        };

        await _classRepo.AddAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Result.Success(entity.Id);
    }
}