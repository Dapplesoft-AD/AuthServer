using Application.Abstractions.Messaging;
using Application.Roles.Delete;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Roles;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("roles/{id:guid}", async (
            Guid id,
            ICommandHandler<DeleteRoleCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteRoleCommand(id);

            SharedKernel.Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                value => Results.NoContent(),
                error => error.ToProblemDetails()
            );
        })
        .WithTags("Roles");
    }
}
