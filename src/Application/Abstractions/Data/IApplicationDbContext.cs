using Domain.Applications;
using Domain.Customers;
using Domain.Permissions;
using Domain.RolePermissions;
using Domain.Roles;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<Applicationapply> Applications { get; }  // ← ADD THIS
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<Role> Roles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
