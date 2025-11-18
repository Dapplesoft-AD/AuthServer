using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.RolePermissions.Update;
public sealed record UpdateRolePermissionCommand(
    Guid RoleId,
    Guid PermissionId,
    Guid NewPermissionId
) : ICommand<Guid>;
