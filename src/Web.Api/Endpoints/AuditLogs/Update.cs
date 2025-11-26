using Application.Abstractions.Messaging;
using Application.AuditLogs.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.AuditLogs;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("AuditLogs/{id:guid}", async (
            Guid id,
            UpdateAuditLogRequest request,
            ICommandHandler<UpdateAuditLogCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateAuditLogCommand(
                AuditLogId: id,
                Action: request.Action,
                Description: request.Description
            );

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                Results.NoContent,
                error => CustomResults.Problem(error)
            );
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
