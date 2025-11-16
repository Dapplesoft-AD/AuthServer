using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.Get;
public sealed record RoleResponse(
    Guid Id,
    string RoleName,
    string Description
);
