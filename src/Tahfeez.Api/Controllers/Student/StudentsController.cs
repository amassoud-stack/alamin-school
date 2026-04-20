using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tahfeez.Application.Features.Student.Commands.ActivateUser;
using Tahfeez.Application.Features.Student.Commands.AddStudentToClass;
using Tahfeez.Application.Features.Student.Commands.PromoteStudent;
using Tahfeez.Application.Features.Student.Commands.TransferStudent;
using Tahfeez.SharedKernal.Coonstants;

namespace Tahfeez.Api.Controllers.Student;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;
    public StudentsController(IMediator mediator) => _mediator = mediator;

    /// <summary>POST /api/students/{id}/activate — Admin only</summary>
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{id:guid}/activate")]
    public async Task<IActionResult> Activate([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new ActivateUserCommand(id));
        if (result.IsFailure) return NotFound(result);
        return Ok(result);
    }

    /// <summary>POST /api/students/{id}/assign-class — Admin, Supervisor</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor}")]
    [HttpPost("{id:guid}/assign-class")]
    public async Task<IActionResult> AssignToClass([FromRoute] Guid id, [FromBody] AssignClassRequest body)
    {
        var result = await _mediator.Send(new AddStudentToClassCommand(id, body.ClassId, body.Level));
        if (result.IsFailure) return BadRequest(result);
        return Ok(result);
    }

    /// <summary>POST /api/students/{id}/transfer — Admin, Supervisor</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor}")]
    [HttpPost("{id:guid}/transfer")]
    public async Task<IActionResult> Transfer([FromRoute] Guid id, [FromBody] TransferRequest body)
    {
        var result = await _mediator.Send(new TransferStudentCommand(id, body.NewClassId));
        if (result.IsFailure) return BadRequest(result);
        return Ok(result);
    }

    /// <summary>POST /api/students/{id}/promote — Teacher, Supervisor, Admin</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor},{Roles.Teacher}")]
    [HttpPost("{id:guid}/promote")]
    public async Task<IActionResult> Promote([FromRoute] Guid id, [FromBody] PromoteRequest body)
    {
        var result = await _mediator.Send(new PromoteStudentCommand(id, body.NewLevel, body.NewClassId));
        if (result.IsFailure) return BadRequest(result);
        return Ok(result);
    }
}

public record AssignClassRequest(Guid ClassId, string Level);
public record TransferRequest(Guid NewClassId);
public record PromoteRequest(string NewLevel, Guid? NewClassId);
