using MediatR;
using Tahfeez.Application.Features.Recitation.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Recitation.Queries.GetRecitationsByStudent;

public record GetRecitationsByStudentQuery(Guid StudentId) : IRequest<Result<IEnumerable<RecitationDto>>>;
