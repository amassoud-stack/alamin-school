using MediatR;
using Tahfeez.Application.Features.Recitation.DTOs;
using Tahfeez.Application.Features.Recitation.Mappings;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Recitation.Queries.GetRecitationsByStudent;

public class GetRecitationsByStudentHandler : IRequestHandler<GetRecitationsByStudentQuery, Result<IEnumerable<RecitationDto>>>
{
    private readonly IRecitationRepository _repo;

    public GetRecitationsByStudentHandler(IRecitationRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<RecitationDto>>> Handle(GetRecitationsByStudentQuery request, CancellationToken cancellationToken)
    {
        var records = await _repo.GetByStudentAsync(request.StudentId, cancellationToken);
        return Result.Success(records.Select(r => r.MapDto()));
    }
}
