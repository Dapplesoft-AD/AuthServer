using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Applications;
using SharedKernel;

namespace Domain.Roles;
public sealed class Role : Entity
{
    public Guid Id { get; set; } // bigint PK

    public string RoleName { get; set; } = string.Empty; // unique

    public string Description { get; set; } = string.Empty;
}
