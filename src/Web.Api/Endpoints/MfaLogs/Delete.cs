using Application.Abstractions.Data;
using Domain.MfaLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Web.Api.Endpoints.MfaLogs;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("MfaLogs/{id:guid}", async (
            Guid id,
            IApplicationDbContext context,
            CancellationToken cancellationToken) =>
        {
            MfaLog? mfaLog = await context.MfaLogs
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (mfaLog is null)
            {
                return Results.NotFound(Result.Failure(MfaLogErrors.NotFound(id)));
            }

            context.MfaLogs.Remove(mfaLog);
            await context.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result.Success());
        })
        .WithTags(Tags.MfaLogs)
        .RequireAuthorization()
        .WithSummary("Delete an MFA Log entry")
        .WithDescription("Deletes an MFA Log entry by Id");
    }
}
