using Application.Abstractions.Messaging;
using Application.RolePermissions.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.RolePermissions;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("role-permissions/{roleId:guid}/permissions/{permissionId:guid}", async (
            Guid roleId,
            Guid permissionId,
            ICommandHandler<DeleteRolePermissionCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteRolePermissionCommand(roleId, permissionId);

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                () => Results.Ok(new { Message = "Role permission deleted successfully." }),
                CustomResults.Problem);
        })
        .WithTags("RolePermissions")
        .RequireAuthorization();
    }
}
