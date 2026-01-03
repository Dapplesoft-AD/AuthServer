using Application.Abstractions.Messaging;
using Application.Address.GetAll;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Address;

public class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.Addresses), async (
                IQueryHandler<GetAddressQuery, List<GetAddressQueryResponse>> handler,
                CancellationToken cancellationToken) =>
        {
            var query = new GetAddressQuery();

            Result<List<GetAddressQueryResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags(Tags.Addresses);
    }
}
