using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.RolePermissions.Create;
public sealed record CreateRolePermissionCommand(
    Guid RoleId,
    Guid PermissionId
) : ICommand<Guid>;
