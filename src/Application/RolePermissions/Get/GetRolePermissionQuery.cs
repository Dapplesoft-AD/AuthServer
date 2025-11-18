using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.RolePermissions.Get;
public sealed record GetRolePermissionQuery(
    Guid RoleId,
    Guid PermissionId
) : IQuery<RolePermissionResponse>;
