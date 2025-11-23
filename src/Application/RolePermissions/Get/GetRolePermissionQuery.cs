using Application.Abstractions.Messaging;

namespace Application.RolePermissions.Get;
public sealed record GetRolePermissionQuery(
    Guid RoleId,
    Guid PermissionId
) : IQuery<RolePermissionResponse>;
