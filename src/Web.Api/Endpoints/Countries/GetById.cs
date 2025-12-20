using Application.Abstractions.Messaging;
using Application.Countries.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Countries;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetById(Base.Countries), async (
            Guid id,
            IQueryHandler<GetCountryByIdQuery, GetCountryByIdResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCountryByIdQuery(id);
            Result<GetCountryByIdResponse> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Countries)
        .RequireAuthorization();
    }
}
