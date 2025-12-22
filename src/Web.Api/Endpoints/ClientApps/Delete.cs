using Application.Abstractions.Messaging;
using Application.ClientApps.Create;
using Application.ClientApps.Delete;
using SharedKernel;

namespace Web.Api.Endpoints.ClientApps;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/client-apps/{clientId}", async (
            string clientId,
            ICommandHandler<DeleteClientCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteClientCommand(clientId);
            Result result = await handler.Handle(command, cancellationToken);
            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.ClientApps)
        .RequireAuthorization();
    }
}
