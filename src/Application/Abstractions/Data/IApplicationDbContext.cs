using Domain.Applications;
using Domain.Customers;
using Domain.Permissions;
using Domain.RolePermissions;
using Domain.Roles;
using Domain.Todos;
using Domain.UserRoles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<Customer> Customers { get; }
 
    DbSet<Permission> Permissions { get; }

    DbSet<Applicationapply> Applications { get; }  // ← ADD THIS
    DbSet<Role> Roles { get; }
    DbSet<RolePermission> RolePermissions { get; }



    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
