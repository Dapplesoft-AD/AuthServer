using Application.Abstractions.Data;
using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Web.Api.Endpoints.MfaSettings;

public static class Update
{
    public static void MapUpdateMfaSettingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/MfaSettings/{id}", async (
            Guid id,
            UpdateMfaSettingRequest request,
            IApplicationDbContext context,
            CancellationToken cancellationToken) =>
        {
            MfaSetting? mfa = await context.MfaSettings
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (mfa is null)
            {
                return Results.NotFound(Result.Failure(
                    Error.NotFound("MfaSetting.NotFound", $"MfaSetting with Id {id} not found.")
                ));
            }

            if (string.IsNullOrWhiteSpace(request.Method))
            {
                return Results.BadRequest("Method is required.");
            }


            bool parsed = Enum.TryParse<MfaMethod>(request.Method, true, out MfaMethod method);
            if (!parsed)
            {
                return Results.BadRequest("Invalid MFA method. Allowed: TOTP, SMS, EMAIL.");
            }


            // Update values
            mfa.UserId = request.UserId;
            mfa.SecretKey = request.SecretKey ?? mfa.SecretKey;
            mfa.BackupCodes = request.BackupCodes ?? mfa.BackupCodes;
            mfa.Method = method;
            mfa.Enabled = request.Enabled;

            await context.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result.Success());
        })
        .WithName("UpdateMfaSetting")
        .WithTags("MfaSettings")
        .RequireAuthorization()
        .WithSummary("Update an MFA Setting")
        .WithDescription("Updates an MFA setting record.");
    }
}

public sealed record UpdateMfaSettingRequest(
    Guid UserId,
    string? SecretKey,
    string? BackupCodes,
    string Method,
    bool Enabled
);
