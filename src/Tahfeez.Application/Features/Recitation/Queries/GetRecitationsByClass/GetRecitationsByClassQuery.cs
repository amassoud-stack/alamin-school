using MediatR;
using Tahfeez.Application.Features.Recitation.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Recitation.Queries.GetRecitationsByClass;

public record GetRecitationsByClassQuery(Guid ClassId, DateOnly Month) : IRequest<Result<IEnumerable<RecitationDto>>>;
