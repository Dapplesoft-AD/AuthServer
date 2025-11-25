using Application.Abstractions.Messaging;
using Application.Businesses.Delete;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Businesses;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/businesses/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteBusinessCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteBusinessCommand(id);
            SharedKernel.Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(
            _ => Results.NoContent(),
            CustomResults.Problem
            );
        })
        .WithTags(Tags.Businesses)
        .RequireAuthorization();
    }
}
