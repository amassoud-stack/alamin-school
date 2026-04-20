using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Recitations;

/// <summary>Represents a single تسميع (recitation) or مراجعة (review) session.</summary>
public class Recitation : AuditableEntity
{
    public Guid StudentId { get; set; }
    public User Student { get; set; } = default!;

    public Guid TeacherId { get; set; }
    public User Teacher { get; set; } = default!;

    public DateOnly Date { get; set; }

    /// <summary>Number of Ayahs recited in this session</summary>
    public int AyahsCount { get; set; }

    public RecitationType Type { get; set; }

    /// <summary>Grade 1–10 awarded by the teacher</summary>
    public int Grade { get; set; }

    public string? Notes { get; set; }
}
