using Application.Abstractions.Messaging;
using Application.AuditLogs.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.AuditLogs;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid UserId { get; set; }
        public Guid BusinessId { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("AuditLogs", async (
            Request request,
            ICommandHandler<CreateAuditLogCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateAuditLogCommand
            {
                UserId = request.UserId,
                BusinessId = request.BusinessId,
                Action = request.Action,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.AuditLogs)
        .RequireAuthorization();
    }
}
