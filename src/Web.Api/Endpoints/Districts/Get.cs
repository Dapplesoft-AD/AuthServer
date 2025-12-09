using Application.Abstractions.Messaging;
using Application.Districts.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Districts;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("Districts", async (
            IQueryHandler<GetDistrictQuery, List<DistrictResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetDistrictQuery();

            Result<List<DistrictResponse>> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Districts)
        .RequireAuthorization()
        .WithSummary("Get all Districts")
        .WithDescription("Returns list of all districts");
    }
}
