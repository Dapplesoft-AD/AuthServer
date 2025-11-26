using Application.Abstractions.Messaging;
using Application.AuditLogs.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.AuditLogs;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("AuditLogs/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteAuditLogCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteAuditLogCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                Results.NoContent,
                error => CustomResults.Problem(error)
            );
        })
        .WithTags(Tags.AuditLogs)
        .RequireAuthorization()
        .WithSummary("Delete an Audit Log entry")
        .WithDescription("Deletes an Audit Log entry by Id");
    }
}
