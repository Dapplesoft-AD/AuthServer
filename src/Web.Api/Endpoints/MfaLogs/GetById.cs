using Microsoft.AspNetCore.Routing;
using Application.Abstractions.Messaging;
using Application.MfaLogs.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaLogs;

public static class GetMfaLogByIdEndpoint
{
    public static void MapGetMfaLogByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/MfaLogs/{id:guid}", async (
            Guid id,
            IQueryHandler<GetMfaLogByIdQuery, MfaLogResponse> handler,
            CancellationToken cancellationToken) =>
        {
            Result<MfaLogResponse> result = await handler.Handle(new GetMfaLogByIdQuery(id), cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.MfaLogs)
        .RequireAuthorization();
    }
}
