using Application.Abstractions.Messaging;

namespace Application.RolePermissions.Create;
public sealed record CreateRolePermissionCommand(
    Guid RoleId,
    Guid PermissionId
) : ICommand<Guid>;
