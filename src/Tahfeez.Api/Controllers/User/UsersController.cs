using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tahfeez.Application.Features.User.Commands.CreateUser;
using Tahfeez.Application.Features.User.Commands.DeleteUser;
using Tahfeez.Application.Features.User.Commands.UpdateUser;
using Tahfeez.Application.Features.User.DTOs;
using Tahfeez.Application.Features.User.Queries.GetAllUsers;
using Tahfeez.Application.Features.User.Queries.GetUserById;
using Tahfeez.Domain.Entities.Roles;
using Tahfeez.SharedKernal.Coonstants;

namespace Tahfeez.Api.Controllers.User;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id,[FromBody] UpdateUserDto userDto)
    {
        var result = await _mediator.Send(new UpdateUserCommand(id, userDto));

        if(result.IsFailure)
            return NotFound(result);

        return Ok(result);
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var result = await _mediator.Send(new CreateUserCommand(userDto));

        if (result.IsFailure) return BadRequest(result);
        return CreatedAtAction(nameof(GetUserById), new { id = result.Value }, result);
    }
}
