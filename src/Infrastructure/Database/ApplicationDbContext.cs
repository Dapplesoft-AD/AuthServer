using Application.Abstractions.Data;
using Domain.Applications;
using Domain.AuditLogs;
using Domain.Businesses;
using Domain.BusinessMembers;
using Domain.Countries;
using Domain.Customers;
using Domain.EmailVerification;
using Domain.MfaLogs;
using Domain.MfaSettings;
using Domain.Otps;
using Domain.PasswordResets;
using Domain.Permissions;
using Domain.RolePermissions;
using Domain.Roles;
using Domain.SmtpConfigs;
using Domain.Todos;
using Domain.Token;
using Domain.UserLoginHistories;
using Domain.UserProfiles;
using Domain.Users;
using Infrastructure.DomainEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<EmailVerifications> EmailVerifications { get; set; }

    public DbSet<PasswordReset> PasswordReset { get; set; }

    public DbSet<Tokens> Tokens { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<UserLoginHistory> UserLoginHistory { get; set; }

    public DbSet<UserProfile> UserProfile { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<Applicationapply> Applications { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Business> Businesses { get; set; }

    public DbSet<BusinessMember> BusinessMembers { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<MfaLog> MfaLogs { get; set; }
    public DbSet<MfaSetting> MfaSettings { get; set; }
    public DbSet<Otp> Otp { get; set; }
    public DbSet<SmtpConfig> SmtpConfig { get; set; }
    public DbSet<Country> Countries { get; set; }

    public new EntityEntry Entry(object entity) => base.Entry(entity);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {


        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }
}
