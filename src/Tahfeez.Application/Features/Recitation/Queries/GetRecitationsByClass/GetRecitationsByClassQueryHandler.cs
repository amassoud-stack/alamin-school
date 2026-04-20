using MediatR;
using Tahfeez.Application.Features.Recitation.DTOs;
using Tahfeez.Application.Features.Recitation.Mappings;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Recitation.Queries.GetRecitationsByClass;

public class GetRecitationsByClassHandler : IRequestHandler<GetRecitationsByClassQuery, Result<IEnumerable<RecitationDto>>>
{
    private readonly IRecitationRepository _repo;

    public GetRecitationsByClassHandler(IRecitationRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<RecitationDto>>> Handle(GetRecitationsByClassQuery request, CancellationToken cancellationToken)
    {
        var records = await _repo.GetByClassAndMonthAsync(request.ClassId, request.Month, cancellationToken);
        return Result.Success(records.Select(r => r.MapDto()));
    }
}
