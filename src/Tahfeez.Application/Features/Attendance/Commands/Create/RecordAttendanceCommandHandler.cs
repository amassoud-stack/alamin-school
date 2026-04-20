using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tahfeez.Domain.Entities.Atendences;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Attendance.Commands.Create;

public class RecordAttendanceCommandHandler : IRequestHandler<RecordAttendanceCommand, Result<Guid>>
{
    private readonly IAttendanceRepository _repo;
    private readonly IUnitOfWork _uow;

    public RecordAttendanceCommandHandler(IAttendanceRepository repo, IUnitOfWork uow)
    {
        _repo = repo; _uow = uow;
    }

    public async Task<Result<Guid>> Handle(RecordAttendanceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Atendence
        {
            UserId = request.recordAttendance.UserId,
            Date = request.recordAttendance.Date,
            Status = request.recordAttendance.Status,
            Notes = request.recordAttendance.Notes
        };
        try
        {
            await _repo.AddAsync(entity, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
        }catch (Exception ex)
        {
            return Result.Failure<Guid>(ex.Message);
        }
        

        return Result.Success(entity.Id);
    }
}

