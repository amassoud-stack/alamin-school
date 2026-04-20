using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Atendences;

public class Atendence : AuditableEntity
{
    public DateOnly Date { get; set; }
    public AttendanceStatus Status { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    /// <summary>Optional notes from the teacher/supervisor</summary>
    public string? Notes { get; set; }
}
