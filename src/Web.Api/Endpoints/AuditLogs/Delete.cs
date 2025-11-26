using Application.Abstractions.Data;
using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Web.Api.Endpoints.AuditLogs;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("AuditLogs/{id:guid}", async (
            Guid id,
            IApplicationDbContext context,
            CancellationToken cancellationToken) =>
        {
            AuditLog? auditLog = await context.AuditLogs
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (auditLog is null)
            {
                return Results.NotFound(Result.Failure(AuditLogErrors.NotFound(id)));
            }

            context.AuditLogs.Remove(auditLog);
            await context.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result.Success());
        })
        .WithTags(Tags.AuditLogs)
        .RequireAuthorization()
        .WithSummary("Delete an Audit Log entry")
        .WithDescription("Deletes an Audit Log entry by Id");
    }
}
