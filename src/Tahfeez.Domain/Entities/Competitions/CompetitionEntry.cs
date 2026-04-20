using Tahfeez.Domain.Entities.Users;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Competitions;

public class CompetitionEntry : AuditableEntity
{
    public Guid CompetitionId { get; set; }
    public Competition Competition { get; set; } = default!;

    public Guid StudentId { get; set; }
    public User Student { get; set; } = default!;

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    /// <summary>Optional ranking / result filled after competition ends</summary>
    public int? Rank { get; set; }
}
