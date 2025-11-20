using Application.Abstractions.Messaging;
using Application.RolePermissions.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.RolePermissions;

internal sealed class Update : IEndpoint
{
    public sealed record Request(Guid NewPermissionId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("role-permissions/{roleId:guid}/permissions/{permissionId:guid}", async (
            Guid roleId,
            Guid permissionId,
            Request request,
            ICommandHandler<UpdateRolePermissionCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateRolePermissionCommand(roleId, permissionId, request.NewPermissionId);

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                updatedRoleId => Results.Ok(new { RoleId = updatedRoleId, Message = "Role permission updated successfully." }),
                CustomResults.Problem);
        })
        .WithTags("RolePermissions")
        .RequireAuthorization();
    }
}
