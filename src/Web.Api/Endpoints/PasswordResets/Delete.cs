using Application.Abstractions.Messaging;
using Application.PasswordResets.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.PasswordResets;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("PasswordResets/{id:guid}", async (
            Guid id,
            ICommandHandler<DeletePasswordResetCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeletePasswordResetCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.PasswordReset)
        .RequireAuthorization();
    }
}
