using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.Permissions.Get;

namespace Application.Permissions.GetAll;
public sealed record GetAllPermissionsQuery() : IQuery<List<PermissionResponse>>;
