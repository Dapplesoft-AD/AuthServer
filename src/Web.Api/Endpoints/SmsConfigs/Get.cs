using Application.Abstractions.Messaging;
using Application.SmsConfigs.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SmsConfigs;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("SmsConfig", async (
            IQueryHandler<GetSmsConfigsQuery, List<SmsConfigResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetSmsConfigsQuery();

            Result<List<SmsConfigResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.SmsConfig)
        .RequireAuthorization();
    }
}
