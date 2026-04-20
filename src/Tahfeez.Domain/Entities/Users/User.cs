using Microsoft.AspNetCore.Identity;
using Tahfeez.Domain.Entities.Classes;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Interfaces;

namespace Tahfeez.Domain.Entities.Users;

public class User : IdentityUser<Guid>, IFullAudit, ISoftDeletable
{
    public string FullName { get; set; } = default!;
    public string? PhoneNumber2 { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Pending;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? ClassId { get; set; }
    public Class? Class { get; set; }
    public string? Level { get; set; }
    public DateTime? StudentJoinDate { get; set; }
    public ICollection<Atendences.Atendence> Attendances { get; set; } = [];
    public ICollection<Subscriptions.Subscription> Subscriptions { get; set; } = [];
}
