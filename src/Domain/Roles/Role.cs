using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Roles;
public class Role : Entity
{
    public Guid Id { get; set; } // Primary Key
    public string RoleName { get; set; } // Example: Admin, Manager, Employee
    public string Description { get; set; } // Optional description
}
