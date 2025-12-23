using Application.Abstractions.Messaging;
using Application.Localities.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Localities;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetById(Base.Localities), async (
            Guid id,
            IQueryHandler<GetLocalityByIdQuery, LocalityResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetLocalityByIdQuery(id);

            Result<LocalityResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Localities)
        .RequireAuthorization();
    }
}

