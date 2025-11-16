using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Roles.Create;
public sealed record CreateRoleCommand(
    string RoleName,
    string Description
) : ICommand<Guid>;
