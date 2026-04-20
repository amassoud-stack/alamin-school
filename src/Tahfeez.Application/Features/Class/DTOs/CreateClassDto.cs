using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Class.DTOs;

public record CreateClassDto(
    string Name,
    ClassType Type,
    ClassMode Mode,
    Guid? TeacherId,
    Guid? AssistantId,
    Guid? SupervisorId
);