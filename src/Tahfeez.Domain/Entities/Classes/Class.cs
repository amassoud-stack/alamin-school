using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Classes;

public class Class : AuditableEntity
{
    public string Name { get; set; } = default!;
    public ClassType Type { get; set; }
    public ClassMode Mode { get; set; }

    public Guid? TeacherId { get; set; }
    public User? Teacher { get; set; }

    public Guid? AssistantId { get; set; }
    public User? Assistant { get; set; }

    public Guid? SupervisorId { get; set; }
    public User? Supervisor { get; set; }

    public ICollection<User> Students { get; set; } = [];
}
