using Tahfeez.Domain.Entities.Payments;
using Tahfeez.Domain.Entities.Users;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Subscriptions;

public class Subscription : AuditableEntity
{
    public SubscriptionType Type { get; set; }
    public SubscriptionMode Mode { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public DateOnly PaymentDate { get; set; }
    public Guid StudentId { get; set; }
    public User Student { get; set; } = default!;
    public ICollection<Payment> Payments { get; set; } = [];
}
