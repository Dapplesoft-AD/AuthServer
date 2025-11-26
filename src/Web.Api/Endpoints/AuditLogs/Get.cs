using Application.Abstractions.Messaging;
using Application.AuditLogs.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.AuditLogs;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("AuditLogs", async (
            IQueryHandler<GetAuditLogQuery, List<AuditLogResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAuditLogQuery();

            Result<List<AuditLogResponse>> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.AuditLogs)
        .RequireAuthorization();
    }
}
