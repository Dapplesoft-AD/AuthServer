using Application.Abstractions.Messaging;
using Application.Projects.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Projects;
internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.GetById(Base.Projects), async (
            Guid id,
            IQueryHandler<GetProjectByIdQuery, GetProjectResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProjectByIdQuery(id);

            Result<GetProjectResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Projects)
        .RequireAuthorization();
    }
}
