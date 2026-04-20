using Tahfeez.Domain.Enums;

namespace Tahfeez.Application.Features.Subscription.DTOs;

public record SubscriptionDto(
    Guid Id,
    Guid StudentId,
    string StudentName,
    decimal Amount,
    SubscriptionType Type,
    SubscriptionMode Mode,
    PaymentMethod PaymentMethod,
    DateOnly PaymentDate
);
