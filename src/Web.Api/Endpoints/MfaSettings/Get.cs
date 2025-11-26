using Application.Abstractions.Messaging;
using Application.MfaSettings.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MfaSettings;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("MfaSettings", async (
            IQueryHandler<GetMfaSettingQuery, List<MfaSettingResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetMfaSettingQuery();

            Result<List<MfaSettingResponse>> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.MfaSettings)
        .RequireAuthorization();
    }
}
