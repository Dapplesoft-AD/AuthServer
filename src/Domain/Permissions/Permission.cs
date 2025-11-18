using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Applications;
using Domain.RolePermissions;
using SharedKernel;

namespace Domain.Permissions;
public sealed class Permission : Entity
{
    public Guid Id { get; set; } 

    public string Code { get; set; } = string.Empty;  

    public string Description { get; set; } = string.Empty;
}
