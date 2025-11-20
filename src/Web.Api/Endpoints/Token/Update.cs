using Application.Abstractions.Messaging;
using Application.Token.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

public sealed class Update : IEndpoint
{
    public sealed class Request
    {
        public Guid App_id { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("Tokens/update/{id:guid}", async (
            Guid id,
            Request request,
            ICommandHandler<UpdateTokenCommand> handler,
            CancellationToken cancellationToken) =>
        {
            // Use the route ID
            var command = new UpdateTokenCommand(id, request.App_id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Token)
        .RequireAuthorization();
    }
}
