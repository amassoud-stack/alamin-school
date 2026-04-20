using Tahfeez.Application.Features.Recitation.DTOs;

namespace Tahfeez.Application.Features.Recitation.Mappings;

internal static class RecitationMappingExtensions
{
    internal static RecitationDto MapDto(this Domain.Entities.Recitations.Recitation recitation) => new(
        recitation.Id,
        recitation.StudentId,
        recitation.Student?.FullName ?? string.Empty,
        recitation.TeacherId,
        recitation.Teacher?.FullName ?? string.Empty,
        recitation.Date,
        recitation.AyahsCount,
        recitation.Type,
        recitation.Grade,
        GradeLabel(recitation.Grade),
        recitation.Notes
    );

    internal static string GradeLabel(int grade) => grade switch
    {
        >= 9 => "ممتاز",
        >= 7 => "جيد جدًا",
        6 => "جيد",
        5 => "مقبول",
        _ => "إعادة المقرر"
    };
}
