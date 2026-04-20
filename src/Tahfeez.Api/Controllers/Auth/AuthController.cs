using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tahfeez.Application.Features.Auth.Commands.Register;
using Tahfeez.Domain.Enums;

namespace Tahfeez.Api.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registers a new user account.
    /// POST /api/auth/register
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        var command = new RegisterCommand(
            request.UserName,
            request.Password,
            request.ConfirmPassword,
            request.Email,
            request.Role);

        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created);
    }
}
