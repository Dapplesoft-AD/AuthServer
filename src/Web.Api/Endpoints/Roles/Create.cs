using Application.Abstractions.Messaging;
using Application.Roles.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Roles;

internal sealed class Create : IEndpoint
{
    public sealed record Request(string RoleName, string Description);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("roles", async (
            Request request,
            ICommandHandler<CreateRoleCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateRoleCommand(
                request.RoleName,
                request.Description
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                roleId => Results.Ok(new { RoleId = roleId, Message = "Role created successfully." }),
                CustomResults.Problem);
        })
        .WithTags("Roles")
        .RequireAuthorization();
    }
}
