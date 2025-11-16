using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Roles.Get;
public sealed record GetRolesQuery() : IQuery<List<RoleResponse>>;
