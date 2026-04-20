using Tahfeez.Domain.Entities.Users;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Competitions;

public class Competition : AuditableEntity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    public Guid CreatedById { get; set; }
    public User CreatedByUser { get; set; } = default!;

    public bool IsOpen { get; set; } = true;
    public ICollection<CompetitionEntry> Entries { get; set; } = [];
}
