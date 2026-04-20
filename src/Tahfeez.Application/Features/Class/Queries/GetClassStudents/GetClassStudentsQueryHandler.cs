using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Queries.GetClassStudents;


public class GetClassStudentsQueryHandler : IRequestHandler<GetClassStudentsQuery, Result<IEnumerable<UserListItemDto>>>
{
    private readonly IClassRepository _classRepo;

    public GetClassStudentsQueryHandler(IClassRepository classRepo) => _classRepo = classRepo;

    public async Task<Result<IEnumerable<UserListItemDto>>> Handle(
        GetClassStudentsQuery request, CancellationToken cancellationToken)
    {
        var @class = await _classRepo.GetByIdAsync(request.ClassId, cancellationToken);
        if (@class is null)
            return Result.Failure<IEnumerable<UserListItemDto>>($"Class '{request.ClassId}' not found.");

        var dtos = @class.Students.Select(u =>
            new UserListItemDto(u.Id, u.FullName, u.Email, u.CreatedAt, u.UpdatedAt, u.Status));

        return Result.Success(dtos);
    }
}
