using Application.Abstractions.Data;
using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Web.Api.Endpoints.MfaSettings;

public static class Delete
{
    public static void MapDeleteMfaSettingEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/MfaSettings/{id}", async (
            Guid id,
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

            context.MfaSettings.Remove(mfa);
            await context.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result.Success());
        })
        .WithTags("MfaSettings")
        .RequireAuthorization()
        .WithSummary("Delete an MFA Setting")
        .WithDescription("Deletes an MFA Setting by Id");
    }
}
