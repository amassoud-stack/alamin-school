using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using Tahfeez.Application.Features.Auth.Commands.Login;
using Tahfeez.Domain.Entities.Users;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Tahfeez.Api.Helpers
{
    public class TokenHelper
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        public TokenHelper(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        /// <summary>
        /// Processes an OpenID Connect token request and returns the authentication result based on the grant type.
        /// </summary>
        /// <remarks>Supports password and refresh token grant types. For unsupported grant types, returns
        /// an error in the authentication properties. The caller should inspect the returned values to determine the
        /// outcome of the token request.</remarks>
        /// <param name="httpContext">The HTTP context containing the OpenID Connect token request. Must not be null.</param>
        /// <returns>A tuple containing the authentication scheme, the authenticated principal if successful, and authentication
        /// properties describing the result. If the grant type is not supported, the principal is null and the
        /// properties contain error details.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the OpenIddict server request cannot be retrieved from the HTTP context.</exception>
        public async Task<(string, ClaimsPrincipal?, AuthenticationProperties?)> Token(HttpContext httpContext)
        {
            var request = httpContext.GetOpenIddictServerRequest()
                ?? throw new InvalidOperationException("The OpenIddict server request cannot be retrieved.");

            if (request.IsPasswordGrantType())
                return await HandlePasswordGrantAsync(request);

            if (request.IsRefreshTokenGrantType())
                return await HandleRefreshTokenGrantAsync(httpContext);

            return(
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                null,
                new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.UnsupportedGrantType,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The specified grant type is not supported."
                }));
        }

        /// <summary>
        /// Processes a password grant request for OpenID Connect authentication and returns an appropriate action result
        /// based on the authentication outcome.
        /// </summary>
        /// <remarks>This method validates user credentials using the password grant type and integrates with
        /// MediatR and ASP.NET Core Identity. It handles authentication errors by returning standardized OpenID Connect
        /// error responses. The method is intended for use within an OpenIddict authorization endpoint.</remarks>
        /// <param name="request">The OpenID Connect request containing the username, password, and requested scopes for the password grant flow.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the password grant authentication. Returns a sign-in
        /// result if authentication succeeds; otherwise, returns a forbidden result with error details.</returns>
        private async Task<(string, ClaimsPrincipal?, AuthenticationProperties?)> HandlePasswordGrantAsync(OpenIddictRequest request)
        {
            // Validate credentials via MediatR (handles lockout, logging, etc.)
            var loginResult = await _mediator.Send(new LoginCommand(request.Username!, request.Password!));
            if (loginResult.IsFailure)
            {
                return(
                        OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        null,
                        new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = loginResult.Error
                        })
                    );
            }

            var user = await _userManager.FindByIdAsync(loginResult.Value);
            if (user is null)
            {
                return(
                        OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        null,
                        new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "User not found."
                        })
                    );
            }

            var principal = await BuildPrincipalAsync(user, request.GetScopes());
            return (OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal, null);
        }

        /// <summary>
        /// Handles a refresh token grant request by validating the refresh token and re-issuing authentication tokens if
        /// the user is valid.
        /// </summary>
        /// <remarks>This method is typically invoked as part of an OpenID Connect or OAuth 2.0 token endpoint to
        /// support the refresh token grant flow. The method ensures that the refresh token is valid and that the associated
        /// user still exists and is active before issuing new tokens.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that
        /// issues new tokens if the refresh token and user are valid; otherwise, a forbidden result with an appropriate
        /// error is returned.</returns>
        private async Task<(string, ClaimsPrincipal?, AuthenticationProperties?)> HandleRefreshTokenGrantAsync(HttpContext httpContext)
        {
            // Authenticate using the existing refresh token
            var result = await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            var userId = result.Principal?.GetClaim(Claims.Subject);

            if (string.IsNullOrEmpty(userId))
            {
                return(
                        OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        null,
                        new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The refresh token is invalid."
                        })
                    );
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null || user.IsDeleted)
            {
                return(
                        OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                        null,
                        new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user no longer exists."
                        })
                    );
            }

            // Re-issue tokens with the same scopes that were in the original token
            var scopes = result.Principal?.GetScopes() ?? [];
            var principal = await BuildPrincipalAsync(user, scopes);
            return (OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal, null);
        }

        /// <summary>
        /// Asynchronously builds a ClaimsPrincipal for the specified user, including core claims and roles, and configures
        /// scopes and claim destinations for token issuance.
        /// </summary>
        /// <remarks>The resulting ClaimsPrincipal always includes the offline_access, openid, email, profile, and
        /// roles scopes to ensure proper token issuance. Claims are assigned destinations so they appear in the appropriate
        /// tokens. This method is typically used in OpenID Connect or OAuth2 authentication flows.</remarks>
        /// <param name="user">The user for whom to build the ClaimsPrincipal. Cannot be null.</param>
        /// <param name="requestedScopes">The collection of scopes requested by the client. Determines which scopes are included in the resulting
        /// ClaimsPrincipal.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a ClaimsPrincipal configured with
        /// the user's claims, roles, and the appropriate scopes for token issuance.</returns>
        private async Task<ClaimsPrincipal> BuildPrincipalAsync(User user, IEnumerable<string> requestedScopes)
        {
            var identity = new ClaimsIdentity(
                authenticationType: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                nameType: Claims.Name,
                roleType: Claims.Role);

            // Core claims
            identity.SetClaim(Claims.Subject, user.Id.ToString());
            identity.SetClaim(Claims.Email, user.Email);
            identity.SetClaim(Claims.Name, user.UserName);

            // Role claims
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                identity.AddClaim(new Claim(Claims.Role, role));

            var principal = new ClaimsPrincipal(identity);

            // Always include offline_access so a refresh token is issued
            var scopes = requestedScopes.Union([Scopes.OfflineAccess, Scopes.OpenId, Scopes.Email, Scopes.Profile, Scopes.Roles]);
            principal.SetScopes(scopes);

            // Set destinations so claims appear in the access/identity tokens
            foreach (var claim in principal.Claims)
                claim.SetDestinations(GetDestinations(claim));

            return principal;
        }

        /// <summary>
        /// Determines the token destinations for the specified claim based on its type.
        /// </summary>
        /// <param name="claim">The claim for which to determine the applicable token destinations. Cannot be null.</param>
        /// <returns>An enumerable collection of destination strings indicating which tokens the claim should be included in. The
        /// collection contains both access and identity token destinations for subject, name, email, or role claims;
        /// otherwise, only the access token destination is included.</returns>
        private static IEnumerable<string> GetDestinations(Claim claim)
        {
            return claim.Type switch
            {
                Claims.Subject or Claims.Name or Claims.Email or Claims.Role
                    => [Destinations.AccessToken, Destinations.IdentityToken],
                _ => [Destinations.AccessToken]
            };
        }
    }
}
