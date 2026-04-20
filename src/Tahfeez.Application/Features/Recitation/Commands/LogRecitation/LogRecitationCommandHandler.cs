using MediatR;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Recitation.Commands.LogRecitation;

public class LogRecitationHandler : IRequestHandler<LogRecitationCommand, Result<Guid>>
{
    private readonly IRecitationRepository _repo;
    private readonly IUnitOfWork _uow;

    public LogRecitationHandler(IRecitationRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Result<Guid>> Handle(LogRecitationCommand request, CancellationToken cancellationToken)
    {
        if (request.Grade is < 1 or > 10)
            return Result.Failure<Guid>("Grade must be between 1 and 10.");

        var entity = new Domain.Entities.Recitations.Recitation
        {
            StudentId = request.StudentId,
            TeacherId = request.TeacherId,
            Date = request.Date,
            AyahsCount = request.AyahsCount,
            Type = request.Type,
            Grade = request.Grade,
            Notes = request.Notes
        };

        await _repo.AddAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success(entity.Id);
    }
}
