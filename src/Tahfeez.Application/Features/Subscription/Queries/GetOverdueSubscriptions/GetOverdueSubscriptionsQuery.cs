using MediatR;
using Tahfeez.Application.Features.Subscription.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Subscription.Queries.GetOverdueSubscriptions;

public record GetOverdueSubscriptionsQuery(int DaysOverdue = 30) : IRequest<Result<IEnumerable<OverdueSubscriptionDto>>>;
