using Application.Abstractions.Messaging;
using Application.Permissions.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Permissions;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("permissions/{id:guid}", async (
            Guid id,
            ICommandHandler<DeletePermissionCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeletePermissionCommand(id);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                () => Results.Ok(new { Message = "Permission deleted successfully." }),
                CustomResults.Problem);
        })
        .WithTags("Permissions")
        .RequireAuthorization();
    }
}
