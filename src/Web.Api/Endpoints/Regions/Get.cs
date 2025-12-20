using Application.Abstractions.Messaging;
using Application.Regions.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Regions;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.Regions), async (
            IQueryHandler<GetRegionQuery, List<RegionResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRegionQuery();

            Result<List<RegionResponse>> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Regions)
        .RequireAuthorization()
        .WithSummary("Get all Regions")
        .WithDescription("Returns list of all regions");
    }
}
