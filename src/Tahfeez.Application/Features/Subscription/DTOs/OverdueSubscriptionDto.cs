namespace Tahfeez.Application.Features.Subscription.DTOs;

public record OverdueSubscriptionDto(
    Guid StudentId,
    string StudentName,
    string? PhoneNumber,
    string? WhatsAppNumber,
    decimal Amount,
    DateOnly LastPaymentDate,
    int DaysOverdue
);
