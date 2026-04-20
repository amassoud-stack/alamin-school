using MediatR;
using Tahfeez.Application.Features.Subscription.DTOs;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Subscription.Queries.GetStudentSubscriptions;

public record GetStudentSubscriptionsQuery(Guid StudentId) : IRequest<Result<IEnumerable<SubscriptionDto>>>;
