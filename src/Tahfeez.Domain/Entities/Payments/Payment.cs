using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Entities.Subscriptions;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Domain.Entities.Payments
{
    public class Payment : AuditableEntity
    {
        public DateTime Date { get; set; }
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
