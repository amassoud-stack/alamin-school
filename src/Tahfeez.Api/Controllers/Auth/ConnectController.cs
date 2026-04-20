using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using Tahfeez.Api.Helpers;
using Tahfeez.Application.Features.Auth.Commands.Login;
using static OpenIddict.Abstractions.OpenIddictConstants;
using AppUser = Tahfeez.Domain.Entities.Users.User;

namespace Tahfeez.Api.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class ConnectController : ControllerBase
{
    private readonly TokenHelper _tokenHelper;

    public ConnectController(TokenHelper tokenHelper)
    {
        _tokenHelper = tokenHelper;
    }

    /// <summary>
    /// Token endpoint — handles password and refresh_token grants.
    /// POST /connect/token
    /// </summary>
    [HttpPost("token")]
    [Consumes("application/x-www-form-urlencoded")]
    public async Task<IActionResult> Token()
    {
        var request = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("The OpenIddict server request cannot be retrieved.");
        var (scheme, claimsPrincipal, authenticationProperties) = await _tokenHelper.Token(HttpContext);
        
        if (!(claimsPrincipal is null))
            return SignIn(claimsPrincipal, scheme);

        return Forbid(
            authenticationSchemes: scheme,
            properties: authenticationProperties);
    }
}
