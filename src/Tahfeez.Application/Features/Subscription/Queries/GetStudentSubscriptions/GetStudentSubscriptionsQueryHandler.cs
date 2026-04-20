using MediatR;
using Tahfeez.Application.Features.Subscription.DTOs;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Subscription.Queries.GetStudentSubscriptions;

public class GetStudentSubscriptionsHandler : IRequestHandler<GetStudentSubscriptionsQuery, Result<IEnumerable<SubscriptionDto>>>
{
    private readonly ISubscriptionRepository _repo;

    public GetStudentSubscriptionsHandler(ISubscriptionRepository repo) => _repo = repo;

    public async Task<Result<IEnumerable<SubscriptionDto>>> Handle(
        GetStudentSubscriptionsQuery request,
        CancellationToken cancellationToken)
    {
        var subs = await _repo.GetByStudentAsync(request.StudentId, cancellationToken);
        var dtos = subs.Select(s => new SubscriptionDto(
            s.Id,
            s.StudentId,
            s.Student?.FullName ?? string.Empty,
            s.Amount,
            s.Type,
            s.Mode,
            s.PaymentMethod,
            s.PaymentDate));
        return Result.Success(dtos);
    }
}
