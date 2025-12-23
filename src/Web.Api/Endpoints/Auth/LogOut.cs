using Application.Abstractions.Messaging;
using Application.Businesses.Create;
using Domain.Businesses;
using Microsoft.AspNetCore.Authentication;

namespace Web.Api.Endpoints.Auth;

public class LogOut : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/logout", async (HttpContext context) =>
        {
            await context.SignOutAsync("AuthCookie");
            return Results.Ok();
        });
    }
}
