using Domain.Applications;
using Domain.Businesses;
using Domain.BusinessMembers;
using Domain.Customers;
using Domain.EmailVerification;
using Domain.PasswordResets;
using Domain.Permissions;
using Domain.RolePermissions;
using Domain.Roles;
using Domain.Todos;
using Domain.Token;
using Domain.UserLoginHistories;
using Domain.UserProfiles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
    DbSet<UserLoginHistory> UserLoginHistory { get; }
    DbSet<UserProfile> UserProfile { get; }
    DbSet<Business> Businesses { get; }
    DbSet<BusinessMember> BusinessMembers { get; }

    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
