using MediatR;
using Tahfeez.Application.Features.Subscription.DTOs;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Subscription.Queries.GetOverdueSubscriptions;

public class GetOverdueSubscriptionsHandler : IRequestHandler<GetOverdueSubscriptionsQuery, Result<IEnumerable<OverdueSubscriptionDto>>>
{
    private readonly ISubscriptionRepository _repo;

    public GetOverdueSubscriptionsHandler(ISubscriptionRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<OverdueSubscriptionDto>>> Handle(
        GetOverdueSubscriptionsQuery request,
        CancellationToken cancellationToken)
    {
        var subs = await _repo.GetOverdueAsync(request.DaysOverdue, cancellationToken);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var dtos = subs.Select(s => new OverdueSubscriptionDto(
            s.StudentId,
            s.Student?.FullName ?? string.Empty,
            s.Student?.PhoneNumber,
            s.Student?.PhoneNumber2,
            s.Amount,
            s.PaymentDate,
            today.DayNumber - s.PaymentDate.DayNumber));

        return Result.Success(dtos);
    }
}
