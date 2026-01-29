using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

public sealed class ClaimsDebug : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/auth/claims", [Authorize(AuthenticationSchemes = "AuthCookie")] (HttpContext ctx) =>
        {
            ClaimsPrincipal user = ctx.User;

            var claims = user.Claims
                .Select(c => new
                {
                    c.Type,
                    c.Value,
                    c.Issuer,
                    c.ValueType
                })
                .ToList();

            return Results.Ok(new
            {
                IsAuthenticated = user.Identity?.IsAuthenticated ?? false,
                user.Identity?.Name,
                Claims = claims
            });
        })
        .WithTags("Auth");
    }
}
