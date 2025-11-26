using Application.Abstractions.Data;
using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Web.Api.Endpoints.AuditLogs;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("AuditLogs/{id:guid}", async (
            Guid id,
            UpdateAuditLogRequest request,
            IApplicationDbContext context,
            CancellationToken cancellationToken) =>
        {
            AuditLog? auditLog = await context.AuditLogs
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (auditLog is null)
            {
                return Results.NotFound(Result.Failure(AuditLogErrors.NotFound(id)));
            }

            if (string.IsNullOrWhiteSpace(request.Action))
            {
                return Results.BadRequest("Action is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return Results.BadRequest("Description is required.");
            }

            auditLog.Action = request.Action;
            auditLog.Description = request.Description;
            auditLog.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);

            return Results.Ok(Result.Success());
        })
        .WithTags(Tags.AuditLogs)
        .RequireAuthorization()
        .WithSummary("Update an Audit Log entry")
        .WithDescription("Updates the Action and Description of an Audit Log");
    }
}

public sealed record UpdateAuditLogRequest(
    string Action,
    string Description
);
