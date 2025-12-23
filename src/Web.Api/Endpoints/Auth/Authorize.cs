using System.Collections.Immutable;
using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

internal sealed class Authorize : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("connect/authorize", async (
            HttpContext httpContext,
            IOpenIddictApplicationManager applicationManager,
            IOpenIddictAuthorizationManager authorizationManager,
            IOpenIddictScopeManager scopeManager) =>
        {
            ILogger<Authorize> logger = httpContext.RequestServices.GetRequiredService<ILogger<Authorize>>();
            logger.LogInformation("Authorize Endpoint Hit. Path: {Path}", httpContext.Request.Path);

            // Resolve the openiddict request
            OpenIddictRequest request = httpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            // Retrieve the user principal stored in the authentication cookie.
            AuthenticateResult result = await httpContext.AuthenticateAsync("AuthCookie");

            if (!result.Succeeded)
            {
                logger.LogInformation("User is NOT authenticated. Redirecting to Login. ReturnUrl: {ReturnUrl}", httpContext.Request.PathBase + httpContext.Request.Path + httpContext.Request.QueryString);
                return Results.Challenge(
                    properties: new AuthenticationProperties
                    {
                        RedirectUri = httpContext.Request.PathBase + httpContext.Request.Path + QueryString.Create(
                            httpContext.Request.HasFormContentType ? httpContext.Request.Form.ToList() : httpContext.Request.Query.ToList())
                    },
                    authenticationSchemes: [ "AuthCookie" ]);
            }

            // Retrieve the profile of the logged in user.
            ClaimsPrincipal principal = result.Principal;
            logger.LogInformation("User IS authenticated. Principal: {Name}", principal.Identity?.Name);

            // Create a new identity and copy the claims from the cookie principal.
            var identity = new ClaimsIdentity(
                authenticationType: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                nameType: OpenIddictConstants.Claims.Name,
                roleType: OpenIddictConstants.Claims.Role);

            // Import claims from cookie and key destinations
            foreach (Claim claim in principal.Claims)
            {
                // Always include claims in the identity token (id_token) and access token (access_token)
                // In a real app, you might want to selective include them based on scopes.
                claim.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken);
                identity.AddClaim(claim);
            }

            // Set the list of scopes granted to the client application.
            ImmutableArray<string> scopes = request.GetScopes();
            identity.SetScopes(scopes);

            // Set the resources that are allowed to be accessed.
            List<string> resources = await scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync();
            identity.SetResources(resources);

            logger.LogInformation("Signing in with OpenIddict. Scopes: {Scopes}", string.Join(", ", scopes));

            // Sign in with OpenIddict
            return Results.SignIn(new ClaimsPrincipal(identity), properties: null, authenticationScheme: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        })
        .ExcludeFromDescription();
    }
}
