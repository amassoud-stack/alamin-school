using MediatR;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Student.Commands.TransferStudent;

public class TransferStudentHandler : IRequestHandler<TransferStudentCommand, Result>
{
    private readonly IUserRepository _userRepo;
    private readonly IClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public TransferStudentHandler(IUserRepository userRepo, IClassRepository classRepo, IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Result> Handle(TransferStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _userRepo.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure("Student not found.");

        var newClass = await _classRepo.GetByIdAsync(request.NewClassId, cancellationToken);
        if (newClass is null) return Result.Failure("Target class not found.");

        student.ClassId = request.NewClassId;
        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
