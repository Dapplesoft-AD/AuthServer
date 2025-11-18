using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.RolePermissions.Get;

namespace Application.RolePermissions.GetByRoleId;
public sealed record GetRolePermissionsByRoleIdQuery(Guid RoleId) : IQuery<List<RolePermissionResponse>>;
