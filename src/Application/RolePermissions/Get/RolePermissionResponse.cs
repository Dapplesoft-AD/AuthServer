using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RolePermissions.Get;
public sealed record RolePermissionResponse(
    Guid RoleId,
    string RoleName,
    Guid PermissionId,
    string PermissionCode,
    string PermissionDescription
);
