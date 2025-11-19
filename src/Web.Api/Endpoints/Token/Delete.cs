using Application.Abstractions.Messaging;
using Application.Token.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("Tokens/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteTokenCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteTokenCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Token)
        .RequireAuthorization();
    }
}
