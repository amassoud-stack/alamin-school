using MediatR;
using Tahfeez.Domain.Repositories;
using Tahfeez.SharedKernal.Common;

namespace Tahfeez.Application.Features.Student.Commands.AddStudentToClass;

public class AddStudentToClassHandler : IRequestHandler<AddStudentToClassCommand, Result>
{
    private readonly IUserRepository _userRepo;
    private readonly IClassRepository _classRepo;
    private readonly IUnitOfWork _uow;

    public AddStudentToClassHandler(IUserRepository userRepo, IClassRepository classRepo, IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _classRepo = classRepo;
        _uow = uow;
    }

    public async Task<Result> Handle(AddStudentToClassCommand request, CancellationToken cancellationToken)
    {
        var student = await _userRepo.GetByIdAsync(request.StudentId, cancellationToken);
        if (student is null) return Result.Failure("Student not found.");

        var @class = await _classRepo.GetByIdAsync(request.ClassId, cancellationToken);
        if (@class is null) return Result.Failure("Class not found.");

        student.ClassId = request.ClassId;
        student.Level = request.Level;
        student.StudentJoinDate ??= DateTime.UtcNow;

        await _uow.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
