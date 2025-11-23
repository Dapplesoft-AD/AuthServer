using Application.Abstractions.Messaging;

namespace Application.RolePermissions.Update;
public sealed record UpdateRolePermissionCommand(
    Guid RoleId,
    Guid PermissionId,
    Guid NewPermissionId
) : ICommand<Guid>;
