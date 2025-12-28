using Domain.Areas;
using Domain.AuditLogs;
using Domain.Countries;
using Domain.Districts;
using Domain.EmailVerification;
using Domain.Localities;
using Domain.Otps;
using Domain.Permissions;
using Domain.Regions;
using Domain.RolePermissions;
using Domain.Roles;
using Domain.SmsConfigs;
using Domain.SmtpConfigs;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<EmailVerifications> EmailVerifications { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<Role> Roles { get; }
    DbSet<AuditLog> AuditLogs { get; }
    DbSet<Otp> Otp { get; }
    DbSet<SmtpConfig> SmtpConfig { get; }
    DbSet<SmsConfig> SmsConfig { get; }
    DbSet<Country> Countries { get; }
    DbSet<Region> Regions { get; }
    DbSet<District> Districts { get; }
    DbSet<Area> Areas { get; }
    DbSet<Locality> Localities { get; }
    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
