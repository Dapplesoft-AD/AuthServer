using Application.Abstractions.Messaging;
using Application.AuditLogs.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.AuditLogs;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("AuditLogs/{id:guid}", async (
            Guid id,
            IQueryHandler<GetAuditLogByIdQuery, AuditLogResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAuditLogByIdQuery(id);

            Result<AuditLogResponse> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.AuditLogs)
        .RequireAuthorization();
    }
}
