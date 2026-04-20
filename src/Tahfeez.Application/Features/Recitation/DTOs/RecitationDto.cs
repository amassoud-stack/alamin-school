using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Recitation.DTOs;

public record RecitationDto(
    Guid Id,
    Guid StudentId,
    string StudentName,
    Guid TeacherId,
    string TeacherName,
    DateOnly Date,
    int AyahsCount,
    RecitationType Type,
    int Grade,
    string GradeLabel,
    string? Notes
);
