using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tahfeez.Application.Features.Class.Commands.AssignStaff;
using Tahfeez.Application.Features.Class.Commands.CreateClass;
using Tahfeez.Application.Features.Class.Commands.DeleteClass;
using Tahfeez.Application.Features.Class.Commands.UpdateClass;
using Tahfeez.Application.Features.Class.DTOs;
using Tahfeez.Application.Features.Class.Queries.GetAllClasses;
using Tahfeez.Application.Features.Class.Queries.GetClassById;
using Tahfeez.Application.Features.Class.Queries.GetClassStudents;
using Tahfeez.SharedKernal.Coonstants;

namespace Tahfeez.Api.Controllers.Class;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IMediator _mediator;
    public ClassesController(IMediator mediator) => _mediator = mediator;

    /// <summary>GET /api/classes — All roles</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllClassesQuery());
        return Ok(result);
    }

    /// <summary>GET /api/classes/{id} — All roles</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetClassByIdQuery(id));
        if (result.IsFailure) return NotFound(result);
        return Ok(result);
    }

    /// <summary>GET /api/classes/{id}/students — Teacher, Assistant, Supervisor, Admin</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor},{Roles.Teacher},{Roles.Assistant}")]
    [HttpGet("{id:guid}/students")]
    public async Task<IActionResult> GetStudents([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetClassStudentsQuery(id));
        if (result.IsFailure) return NotFound(result);
        return Ok(result);
    }

    /// <summary>POST /api/classes — Admin, Supervisor</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor}")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClassDto dto)
    {
        var result = await _mediator.Send(new CreateClassCommand(dto));
        if (result.IsFailure) return BadRequest(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result);
    }

    /// <summary>PUT /api/classes/{id} — Admin, Supervisor</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor}")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClassDto dto)
    {
        var result = await _mediator.Send(new UpdateClassCommand(id, dto));
        if (result.IsFailure) return NotFound(result);
        return Ok(result);
    }

    /// <summary>DELETE /api/classes/{id} — Admin, Supervisor</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor}")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteClassCommand(id));
        if (result.IsFailure) return NotFound(result);
        return NoContent();
    }

    /// <summary>PUT /api/classes/{id}/staff — Admin, Supervisor</summary>
    [Authorize(Roles = $"{Roles.Admin},{Roles.Supervisor}")]
    [HttpPut("{id:guid}/staff")]
    public async Task<IActionResult> AssignStaff([FromRoute] Guid id, [FromBody] AssignStaffDto dto)
    {
        var result = await _mediator.Send(new AssignStaffCommand(id, dto));
        if (result.IsFailure) return NotFound(result);
        return Ok(result);
    }
}
