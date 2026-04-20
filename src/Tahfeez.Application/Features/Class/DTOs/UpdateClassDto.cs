using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Class.DTOs;
public record UpdateClassDto(
    string Name,
    ClassType Type,
    ClassMode Mode
);
