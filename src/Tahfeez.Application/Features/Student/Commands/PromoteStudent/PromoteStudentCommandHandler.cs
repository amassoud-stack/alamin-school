using MediatR;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Student.Commands.PromoteStudent;

public class PromoteStudentHandler : IRequestHandler<PromoteStudentCommand, Result>
{
    private readonly IUserRepository _userRepo;
    private readonly IUnitOfWork _uow;

    public PromoteStudentHandler(IUserRepository userRepo, IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _uow = uow;
    }

    public async Task<Result> Handle(PromoteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _userRepo.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure("Student not found.");

        student.Level = request.NewLevel;
        if (request.NewClassId.HasValue)
        {
            student.ClassId = request.NewClassId;
        }

        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
