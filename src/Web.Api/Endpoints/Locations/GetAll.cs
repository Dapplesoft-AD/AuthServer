using Application.Abstractions.Messaging;
using Application.Locations.GetAll;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Locations;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.Locations), async (
                IQueryHandler<GetLocationQuery, List<GetLocationQueryResponse>> handler,
                CancellationToken cancellationToken) =>
        {
            var query = new GetLocationQuery();

            Result<List<GetLocationQueryResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Locations);
    }
}
