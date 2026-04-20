using Microsoft.AspNetCore.Identity;
using Tahfeez.SharedKernal.Interfaces;

namespace Tahfeez.Domain.Entities.Roles;
public class Role : IdentityRole<Guid>, IFullAudit, ISoftDeletable
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}

