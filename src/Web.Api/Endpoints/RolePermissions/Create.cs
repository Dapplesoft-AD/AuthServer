using Application.Abstractions.Messaging;
using Application.RolePermissions.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.RolePermissions;

internal sealed class Create : IEndpoint
{
    public sealed record Request(Guid RoleId, Guid PermissionId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("role-permissions", async (
            Request request,
            ICommandHandler<CreateRolePermissionCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateRolePermissionCommand(
                request.RoleId,
                request.PermissionId
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                roleId => Results.Ok(new { RoleId = roleId, Message = "Role permission assigned successfully." }),
                CustomResults.Problem);
        })
        .WithTags("RolePermissions")
        .RequireAuthorization();
    }
}
