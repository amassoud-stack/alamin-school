using Tahfeez.SharedKernal.Interfaces;

namespace Tahfeez.SharedKernal.Common;

public abstract class AuditableEntity : BaseEntity ,ISoftDeletable, IFullAudit
{
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
