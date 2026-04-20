using MediatR;
using Tahfeez.Application.Features.Class.DTOs;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Class.Queries.GetAllClasses;

public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, Result<IEnumerable<ClassListItemDto>>>
{
    private readonly IClassRepository _classRepo;

    public GetAllClassesQueryHandler(IClassRepository classRepo) => _classRepo = classRepo;

    public async Task<Result<IEnumerable<ClassListItemDto>>> Handle(
        GetAllClassesQuery request, CancellationToken cancellationToken)
    {
        var classes = await _classRepo.GetAllAsync(cancellationToken);
        var dtos = classes.Select(c => new ClassListItemDto(
            c.Id,
            c.Name,
            c.Type,
            c.Mode,
            c.Students.Count));
        return Result.Success(dtos);
    }
}