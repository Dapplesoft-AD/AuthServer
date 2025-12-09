using Application.Abstractions.Messaging;
using Application.Regions.Get;
using Application.Regions.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Regions;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("Regions/{id:guid}", async (
            Guid id,
            IQueryHandler<GetRegionByIdQuery, RegionResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRegionByIdQuery(id);

            Result<RegionResponse> result =
                await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Regions)
        .RequireAuthorization()
        .WithSummary("Get Region by Id")
        .WithDescription("Returns a region by Id");
    }
}
