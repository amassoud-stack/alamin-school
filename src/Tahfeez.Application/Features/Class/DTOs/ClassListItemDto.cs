using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Class.DTOs;


public record ClassListItemDto(
    Guid Id,
    string Name,
    ClassType Type,
    ClassMode Mode,
    int StudentCount
);