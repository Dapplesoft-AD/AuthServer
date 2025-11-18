using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Permissions.Update;
public sealed record UpdatePermissionCommand(
    Guid Id,
    string Code,
    string Description
) : ICommand<Guid>;
