using Domain.Applications;
using Domain.Customers;
using Domain.EmailVerification;
using Domain.PasswordResets;
using Domain.Todos;
using Domain.Token;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<EmailVerifications> EmailVerifications { get; }
    DbSet<PasswordReset> PasswordReset { get; }
    DbSet<Tokens> Tokens { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<Applicationapply> Applications { get; }  // ← ADD THIS
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<Role> Roles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
