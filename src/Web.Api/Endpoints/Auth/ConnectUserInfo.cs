using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

internal sealed class ConnectUserInfo : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("connect/userinfo", async (HttpContext httpContext) =>
        {
            AuthenticateResult result = await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            if (result.Principal is null)
            {
                return Results.Challenge(
                    authenticationSchemes: new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidToken,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The specified access token is invalid."
                    }));
            }

            var claims = new Dictionary<string, object>(StringComparer.Ordinal);

            string? subject = result.Principal.GetClaim(OpenIddictConstants.Claims.Subject);
            string? email = result.Principal.GetClaim(OpenIddictConstants.Claims.Email);
            string? name = result.Principal.GetClaim(OpenIddictConstants.Claims.Name);

            if (!string.IsNullOrEmpty(subject))
            {
                claims[OpenIddictConstants.Claims.Subject] = subject;
            }

            if (!string.IsNullOrEmpty(email))
            {
                claims[OpenIddictConstants.Claims.Email] = email;
            }

            if (!string.IsNullOrEmpty(name))
            {
                claims[OpenIddictConstants.Claims.Name] = name;
            }

            // debugging, return ALL claims
            /*
            foreach (var claim in result.Principal.Claims)
            {
                 if (!claims.ContainsKey(claim.Type))
                 {
                     claims[claim.Type] = claim.Value;
                 }
            }
            */

            return Results.Ok(claims);
        })
        .ExcludeFromDescription();
    }
}
