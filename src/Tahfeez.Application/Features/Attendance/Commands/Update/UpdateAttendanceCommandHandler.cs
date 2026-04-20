using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Commands.Update;
public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, Result>
{
    private readonly IAttendanceRepository _repo;
    private readonly IUnitOfWork _uow;

    public UpdateAttendanceCommandHandler(IAttendanceRepository repo, IUnitOfWork uow)
    {
        _repo = repo; _uow = uow;
    }

    public async Task<Result> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.id, cancellationToken);
        if (entity is null) return Result.Failure("Attendance record not found.");

        entity.Status = request.updateAttendance.Status;
        entity.Notes = request.updateAttendance.Notes;
        await _repo.UpdateAsync(entity, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
