using Microsoft.AspNetCore.Routing;
using Application.Abstractions.Messaging;
using Application.AuditLogs.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.AuditLogs;

public static class GetAuditLogByIdEndpoint
{
    public static void MapGetAuditLogByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/AuditLogs/{id:guid}", async (
            Guid id,
            IQueryHandler<GetAuditLogByIdQuery, AuditLogResponse> handler,
            CancellationToken cancellationToken) =>
        {
            // Explicit type
            Result<AuditLogResponse> result = await handler.Handle(new GetAuditLogByIdQuery(id), cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);

        })
        .WithTags(Tags.AuditLogs)
        .RequireAuthorization();
    }
}
