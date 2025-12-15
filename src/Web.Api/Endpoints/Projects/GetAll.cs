using Application.Abstractions.Messaging;
using Application.Projects.Get;
using Application.Projects.GetAll;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Projects;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetAll(Base.Projects), async (
            IQueryHandler<GetAllProjectsQuery, List<GetAllProjectsResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllProjectsQuery();
            Result<List<GetAllProjectsResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Projects)
        .RequireAuthorization();
    }
}
