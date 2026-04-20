using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Badges;

/// <summary>Monthly badge (وسم) awarded to a student based on total score.</summary>
public class Badge : AuditableEntity
{
    public Guid StudentId { get; set; }
    public User Student { get; set; } = default!;

    public BadgeType Type { get; set; }

    /// <summary>First day of the month the badge was awarded for</summary>
    public DateOnly Month { get; set; }

    /// <summary>Total monthly score that triggered this badge</summary>
    public int TotalScore { get; set; }
}
