using Application.Abstractions.Messaging;
using Application.Roles.Create;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Roles;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("Roles", async (
            CreateRoleCommand request,
            ICommandHandler<CreateRoleCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateRoleCommand(
                request.RoleName,
                request.Description
            );

            SharedKernel.Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                id => Results.Created($"/roles/{id}", new { id }),
                error => error.ToProblemDetails()
            );
        })
        .WithTags("Roles");
    }
}
