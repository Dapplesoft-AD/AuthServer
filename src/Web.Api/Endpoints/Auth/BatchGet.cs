using System.Text;
using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using Infrastructure.Database;
using Microsoft.Extensions.Primitives;

namespace Web.Api.Endpoints.Auth;

public sealed class BatchGet : IEndpoint
{
    public sealed record Request(List<Guid> UserIds);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/project-users/batch", async (
            HttpContext http,
            [FromBody] Request request,
            IOpenIddictApplicationManager appManager,
            IUserContext context,
            ApplicationDbContext db,
            CancellationToken ct) =>
        {
            // 1) Read client credentials (Basic base64(clientId:secret))
            if (!TryReadBasicAuth(http, out string clientId, out string clientSecret))
            {
                return Results.Unauthorized();
            }

            // 2) Validate clientId + secret using OpenIddictApplications (DB)
            object application = await appManager.FindByClientIdAsync(clientId, ct);
            if (application is null)
            {
                return Results.Unauthorized();
            }

            bool valid = await appManager.ValidateClientSecretAsync(application, clientSecret, ct);
            if (!valid)
            {
                return Results.Unauthorized();
            }

            // 3) Validate payload
            if (request?.UserIds is null || request.UserIds.Count == 0)
            {
                return Results.BadRequest(new { error = "UserIds is required." });
            }

            if (request.UserIds.Count > 200)
            {
                return Results.BadRequest(new { error = "Max 200 ids per request." });
            }

            // 4) Fetch users (you said “any project can access any user for now”)
            var users = await db.Set<User>() // <-- replace with your auth user entity
                .AsNoTracking()
                .Where(u => request.UserIds.Contains(u.Id))
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.FullName, // adjust property names
                    u.Phone,
                    u.Status,
                    u.CreatedAt,
                    u.UpdatedAt
                })
                .ToListAsync(ct);

            return Results.Ok(users);
        })
        .WithTags("ProjectUsers");
    }

    private static bool TryReadBasicAuth(HttpContext http, out string clientId, out string clientSecret)
    {
        clientId = "";
        clientSecret = "";

        if (!http.Request.Headers.TryGetValue("Authorization", out StringValues headerValues))
        {
            return false;
        }

        string header = headerValues.ToString();
        if (!header.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        try
        {
            string encoded = header["Basic ".Length..].Trim();
            string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            string[] parts = decoded.Split(':', 2);
            if (parts.Length != 2)
            {
                return false;
            }

            clientId = parts[0];
            clientSecret = parts[1];
            return true;
        }
        catch
        {
            return false;
        }
    }
}
