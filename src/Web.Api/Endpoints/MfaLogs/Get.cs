using Application.Abstractions.Messaging;
using Application.MfaLogs.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaLogs;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("mfalogs", async (
            IQueryHandler<GetMfaLogQuery, List<MfaLogResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMfaLogQuery();

            Result<List<MfaLogResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                logs => Results.Ok(new
                {
                    Success = true,
                    Data = logs,
                    Message = logs.Count > 0
                        ? $"{logs.Count} MFA log(s) found."
                        : "No MFA logs found."
                }),
                error => CustomResults.Problem(error)
            );
        })
        .WithTags(Tags.MfaLogs)
        .RequireAuthorization()
        .WithSummary("Get MFA logs for current user")
        .WithDescription("Retrieves all MFA authentication logs for the currently authenticated user, ordered by most recent login time.");
    }
}
