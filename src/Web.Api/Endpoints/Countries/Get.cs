using Application.Abstractions.Messaging;
using Application.Countries.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Countries;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.Countries), async (
            IQueryHandler<GetCountriesQuery, List<GetCountryResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCountriesQuery();
            Result<List<GetCountryResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}
