using Application.Abstractions.Messaging;
using Application.MfaLogs.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaLogs;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("MfaLogs", async (
            IQueryHandler<GetMfaLogQuery, List<MfaLogResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMfaLogQuery();

            Result<List<MfaLogResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                logs => Results.Ok(logs),
                error => CustomResults.Problem(error)
            );
        })
        .WithTags(Tags.MfaLogs)
        .RequireAuthorization()
        .WithSummary("Get MFA logs for current user")
        .WithDescription("Retrieves all MFA authentication logs for the currently authenticated user, ordered by most recent login time.");
    }
}
