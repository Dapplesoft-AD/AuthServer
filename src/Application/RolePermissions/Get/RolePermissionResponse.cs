namespace Application.RolePermissions.Get;
public sealed record RolePermissionResponse(
    Guid RoleId,
    string RoleName,
    Guid PermissionId,
    string PermissionCode,
    string PermissionDescription
);
