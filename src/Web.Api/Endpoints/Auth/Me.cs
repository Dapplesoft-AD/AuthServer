using System.Security.Claims;
using Application.Abstractions.Authentication;
using OpenIddict.Abstractions;

namespace Web.Api.Endpoints.Auth;

public class Me : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("auth/me", (IUserContext context, ClaimsPrincipal user) =>
        {
            if (!context.IsAuthenticated)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new
            {
                id = user.FindFirstValue(OpenIddictConstants.Claims.Subject),
                email = user.FindFirstValue(OpenIddictConstants.Claims.Email)
            });
        });
    }
}
