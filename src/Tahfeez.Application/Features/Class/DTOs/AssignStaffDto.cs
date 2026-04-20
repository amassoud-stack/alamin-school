namespace Tahfeez.Application.Features.Class.DTOs;

public record AssignStaffDto(
    Guid? TeacherId,
    Guid? AssistantId,
    Guid? SupervisorId
);
