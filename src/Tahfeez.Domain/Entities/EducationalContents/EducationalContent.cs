using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.EducationalContents;

public class EducationalContent : AuditableEntity
{
    public string Title { get; set; } = default!;

    /// <summary>YouTube embed URL (e.g. https://www.youtube.com/embed/xxxx)</summary>
    public string YoutubeUrl { get; set; } = default!;

    public ContentCategory Category { get; set; }

    public Guid UploadedById { get; set; }
    public User UploadedBy { get; set; } = default!;

    public string? Description { get; set; }
}
