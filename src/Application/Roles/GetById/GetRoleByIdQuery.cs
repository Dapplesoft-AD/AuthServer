using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.Roles.Get;
namespace Application.Roles.GetById;
public sealed record GetRoleByIdQuery(Guid Id) : IQuery<RoleResponse>;
