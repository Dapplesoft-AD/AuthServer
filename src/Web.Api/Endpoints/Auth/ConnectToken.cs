using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

internal sealed class ConnectToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("connect/token", async (
            HttpContext httpContext,
            IOpenIddictScopeManager scopeManager) =>
        {
            OpenIddictRequest request = httpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            if (request.IsAuthorizationCodeGrantType())
            {
                // Retrieve the claims principal stored in the authorization code
                AuthenticateResult result = await httpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                // Import the principal from the authorization code
                var identity = new ClaimsIdentity(
                    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    OpenIddictConstants.Claims.Name,
                    OpenIddictConstants.Claims.Role);

                // Copy claims and set destinations
                foreach (Claim claim in result.Principal!.Claims)
                {
                    // Create a new claim to avoid reference issues, and set destinations
                    var newClaim = new Claim(claim.Type, claim.Value, claim.ValueType, claim.Issuer);
                    
                    // Ensure the claims are included in the access token
                    newClaim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
                    
                    identity.AddClaim(newClaim);
                }

                // Return the SignInResult
                return Results.SignIn(new ClaimsPrincipal(identity), properties: null, authenticationScheme: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
            
            if (request.IsClientCredentialsGrantType())
            {
                // For client credentials (server to server), the "user" is the client itself...
                var identity = new ClaimsIdentity(
                    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    OpenIddictConstants.Claims.Name,
                    OpenIddictConstants.Claims.Role);
                
                // Add the client_id as the subject
                identity.AddClaim(OpenIddictConstants.Claims.Subject, request.ClientId!);
                identity.AddClaim(OpenIddictConstants.Claims.Name, request.ClientId!);

                // Set scopes
                identity.SetScopes(request.GetScopes());
                
                return Results.SignIn(new ClaimsPrincipal(identity), properties: null, authenticationScheme: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        })
        .ExcludeFromDescription();
    }
}
