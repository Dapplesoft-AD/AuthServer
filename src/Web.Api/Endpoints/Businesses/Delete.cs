using Application.Abstractions.Messaging;
using Application.Businesses.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Businesses;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/businesses/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteBusinessCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteBusinessCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Businesses)
        .RequireAuthorization();
    }
}
