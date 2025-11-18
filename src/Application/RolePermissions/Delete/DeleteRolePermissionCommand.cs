using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.RolePermissions.Delete;
public sealed record DeleteRolePermissionCommand(
    Guid RoleId,
    Guid PermissionId
) : ICommand;
