using Application.Abstractions.Messaging;
using Application.Projects.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Projects;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Delete(Base.Projects), async (
            Guid id,
            ICommandHandler<DeleteProjectCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteProjectCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                () => Results.Ok(new { Message = "Projects deleted successfully." }),
                CustomResults.Problem);
        })
        .WithTags(Tags.Projects)
        .RequireAuthorization();
    }
}
