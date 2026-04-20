using MediatR;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Recitation.Commands.LogRecitation;

public record LogRecitationCommand(
    Guid StudentId,
    Guid TeacherId,
    DateOnly Date,
    int AyahsCount,
    RecitationType Type,
    int Grade,
    string? Notes
) : IRequest<Result<Guid>>;
