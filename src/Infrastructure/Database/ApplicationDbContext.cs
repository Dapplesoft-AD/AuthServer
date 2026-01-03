using Application.Abstractions.Data;
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
using Domain.SubDistricts;
using Domain.Todos;
using Domain.UserRoles;
using Domain.Users;
using Infrastructure.Database.Seed;
using Infrastructure.Database.Seed.AddressSeed;
using Infrastructure.DomainEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher
) : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<EmailVerifications> EmailVerifications { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Otp> Otp { get; set; }
    public DbSet<SmtpConfig> SmtpConfig { get; set; }
    public DbSet<SmsConfig> SmsConfig { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<SubDistrict> SubDistricts { get; set; }
    public DbSet<Locality> Localities { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public new EntityEntry Entry(object entity) => base.Entry(entity);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);

        UsersAndRoleSeed.Apply(modelBuilder);

        CountriesSeed.Apply(modelBuilder);
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
