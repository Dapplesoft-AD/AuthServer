using Application.Abstractions.Messaging;
using Application.Regions.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Regions;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Delete(Base.Regions), async (
            Guid id,
            ICommandHandler<DeleteRegionCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteRegionCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                Results.NoContent,
                error => CustomResults.Problem(error)
            );
        })
        .WithTags(Tags.Regions)
        .RequireAuthorization()
        .WithSummary("Delete Region")
        .WithDescription("Deletes a region by Id.");
    }
}
