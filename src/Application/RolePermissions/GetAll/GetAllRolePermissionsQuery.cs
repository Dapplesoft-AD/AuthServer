using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.RolePermissions.Get;

namespace Application.RolePermissions.GetAll;
public sealed record GetAllRolePermissionsQuery() : IQuery<List<RolePermissionResponse>>;
