using Application.Abstractions.Messaging;

namespace Application.RolePermissions.Delete;
public sealed record DeleteRolePermissionCommand(
    Guid RoleId,
    Guid PermissionId
) : ICommand;
