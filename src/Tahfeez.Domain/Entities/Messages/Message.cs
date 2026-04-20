using Tahfeez.Domain.Entities.Users;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Messages;

public class Message : AuditableEntity
{
    public Guid SenderId { get; set; }
    public User Sender { get; set; } = default!;

    public Guid ReceiverId { get; set; }
    public User Receiver { get; set; } = default!;

    public string Content { get; set; } = default!;

    public bool IsRead { get; set; } = false;

    /// <summary>Optional parent message for threaded replies</summary>
    public Guid? ParentMessageId { get; set; }
    public Message? ParentMessage { get; set; }
}
