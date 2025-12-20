using Application.Abstractions.Messaging;
using Application.Areas.Get;
using Application.Areas.GetAll;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Areas;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.Areas), async (
            IQueryHandler<GetAllAreasQuery, List<AreaResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllAreasQuery();
            Result<List<AreaResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Areas)
        .RequireAuthorization();
    }
}

