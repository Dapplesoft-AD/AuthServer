using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.Permissions.Get;

namespace Application.Permissions.GetById;
public sealed record GetPermissionByIdQuery(Guid Id) : IQuery<PermissionResponse>;
