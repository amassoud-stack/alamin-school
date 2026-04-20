using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Class.DTOs;

public record ClassDto(
    Guid Id,
    string Name,
    ClassType Type,
    ClassMode Mode,
    Guid? TeacherId,
    string? TeacherName,
    Guid? AssistantId,
    string? AssistantName,
    Guid? SupervisorId,
    string? SupervisorName,
    int StudentCount
);