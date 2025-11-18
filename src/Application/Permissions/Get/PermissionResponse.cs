using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permissions.Get;
public sealed record PermissionResponse(
    Guid Id,
    string Code,
    string Description
);
