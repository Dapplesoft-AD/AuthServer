using Application.Abstractions.Messaging;
using Application.Areas.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Areas;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("areas/{id:Guid}", async (
            Guid id,
            ICommandHandler<DeleteAreaCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteAreaCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                () => Results.Ok(new { Message = "Area deleted successfully." }),
                CustomResults.Problem);
        })
        .WithTags(Tags.Areas)
        .RequireAuthorization();
    }
}
