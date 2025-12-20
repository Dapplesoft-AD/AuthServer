using Application.Abstractions.Messaging;
using Application.Localities.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Localities;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiRoutes.Delete(Base.Localities), async (
            Guid id,
            ICommandHandler<DeleteLocalityCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteLocalityCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                () => Results.Ok(new { Message = "Locality deleted successfully." }),
                CustomResults.Problem);
        })
        .WithTags(Tags.Localities)
        .RequireAuthorization();
    }
}
