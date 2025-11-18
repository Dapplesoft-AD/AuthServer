<<<<<<< HEAD
﻿using Domain.Applications;
using Domain.Customers;
using Domain.Permissions;
using Domain.RolePermissions;
using Domain.Roles;
using Domain.Todos;
using Domain.UserRoles;
=======
﻿using Domain.Customers;
using Domain.Todos;
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
<<<<<<< HEAD
    DbSet<UserRole> UserRoles { get; }
    DbSet<Customer> Customers { get; }
 
    DbSet<Permission> Permissions { get; }

    DbSet<Applicationapply> Applications { get; }  // ← ADD THIS
    DbSet<Role> Roles { get; }
    DbSet<RolePermission> RolePermissions { get; }


=======

    DbSet<Customer> Customers { get; }
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
