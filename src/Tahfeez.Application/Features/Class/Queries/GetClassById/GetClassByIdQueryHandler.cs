using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.Class.DTOs;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Queries.GetClassById;

public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, Result<ClassDto>>
{
    private readonly IClassRepository _classRepo;

    public GetClassByIdQueryHandler(IClassRepository classRepo) => _classRepo = classRepo;

    public async Task<Result<ClassDto>> Handle(
        GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        var c = await _classRepo.GetByIdAsync(request.ClassId, cancellationToken);
        if (c is null)
            return Result.Failure<ClassDto>($"Class '{request.ClassId}' not found.");

        var dto = new ClassDto(
            c.Id, c.Name, c.Type, c.Mode,
            c.TeacherId, c.Teacher?.FullName,
            c.AssistantId, c.Assistant?.FullName,
            c.SupervisorId, c.Supervisor?.FullName,
            c.Students.Count);

        return Result.Success(dto);
    }
}
