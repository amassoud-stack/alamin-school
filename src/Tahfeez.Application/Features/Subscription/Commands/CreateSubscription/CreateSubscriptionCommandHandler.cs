using MediatR;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Subscription.Commands.CreateSubscription;

public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionCommand, Result<Guid>>
{
    private readonly ISubscriptionRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateSubscriptionHandler(ISubscriptionRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Subscriptions.Subscription
        {
            StudentId = request.StudentId,
            Amount = request.Amount,
            Type = request.Type,
            Mode = request.Mode,
            PaymentMethod = request.PaymentMethod,
            PaymentDate = request.PaymentDate
        };

        await _repo.AddAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success(entity.Id);
    }
}
