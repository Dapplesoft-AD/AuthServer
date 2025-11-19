using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Roles.Update;
public sealed record UpdateRoleCommand(
    Guid Id,
    string RoleName,
    string Description
) : ICommand<Guid>;
