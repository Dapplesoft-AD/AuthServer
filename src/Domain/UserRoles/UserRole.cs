using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Applications;
using Domain.Roles;
using Domain.Users;
using SharedKernel;

namespace Domain.UserRoles;
public sealed class UserRole : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public Guid ApplicationId { get; set; }
    public Applicationapply Application { get; set; } = null!;
    public DateTime AssignedAt { get; set; }
}
