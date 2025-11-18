using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Permissions.Create;
public sealed record CreatePermissionCommand(
    string Code,
    string Description
) : ICommand<Guid>;
