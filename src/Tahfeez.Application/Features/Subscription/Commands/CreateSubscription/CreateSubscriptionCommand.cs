using MediatR;
using Tahfeez.Domain.Enums;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Subscription.Commands.CreateSubscription;

public record CreateSubscriptionCommand(
    Guid StudentId,
    decimal Amount,
    SubscriptionType Type,
    SubscriptionMode Mode,
    PaymentMethod PaymentMethod,
    DateOnly PaymentDate
) : IRequest<Result<Guid>>;
