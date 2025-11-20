using Domain.Permissions;
using Domain.Roles;

namespace Domain.RolePermissions;
public sealed class RolePermission
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}
