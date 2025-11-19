using Microsoft.AspNetCore.Routing;
using Application.Abstractions.Messaging;
using Application.MfaLogs.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaLogs;

public static class GetMfaLogsEndpoint
{
    public static void MapGetMfaLogsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/MfaLogs", async (
            IQueryHandler<GetMfaLogQuery, List<MfaLogResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            Result<List<MfaLogResponse>> result = await handler.Handle(new GetMfaLogQuery(), cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.MfaLogs)
        .RequireAuthorization();
    }
}
