using Application.Abstractions.Messaging;
using Application.Localities.Get;
using Application.Localities.GetAll;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Localities;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.Localities), async (
            IQueryHandler<GetAllLocalitiesQuery, List<LocalityResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllLocalitiesQuery();
            Result<List<LocalityResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Localities)
        .RequireAuthorization();
    }
}
