using System.Collections.Generic;
using Application.Abstractions.Data;
using Domain.Applications;
using Domain.Areas;
using Domain.AuditLogs;
using Domain.Businesses;
using Domain.BusinessMembers;
using Domain.Countries;
using Domain.Customers;
using Domain.Districts;
using Domain.EmailVerification;
using Domain.Localities;
using Domain.MfaLogs;
using Domain.MfaSettings;
using Domain.Otps;
using Domain.PasswordResets;
using Domain.Permissions;
using Domain.Regions;
using Domain.RolePermissions;
using Domain.Roles;
using Domain.SmsConfigs;
using Domain.SmtpConfigs;
using Domain.Todos;
using Domain.Token;
using Domain.UserLoginHistories;
using Domain.UserProfiles;
using Domain.Users;
using Infrastructure.Authentication;
using Infrastructure.DomainEvents;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Org.BouncyCastle.Crypto.Generators;
using SharedKernel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    public DbSet<SmsConfig> SmsConfig { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Area> Areas { get; set; }
    public DbSet<Locality> Localities { get; set; }
    public new EntityEntry Entry(object entity) => base.Entry(entity);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);

        //var passwordHasher = new PasswordHasher();
        //Console.WriteLine(passwordHasher.Hash("Admin123"));
        //Console.WriteLine(passwordHasher.Hash("User123"));
        //Console.WriteLine(passwordHasher.Hash("Demo123"));
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RoleName = "Administrator",
                Description = "System Administrator with full access"
            },
            new Role
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                RoleName = "Support",
                Description = "Support Engineers"
            },
            new Role
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                RoleName = "Analytics",
                Description = "Helps in Analysis"
            },
            new Role
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                RoleName = "PaymentAdmin",
                Description = "Asses the payments"
            },
            new Role
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                RoleName = "Client",
                Description = "Common User"
            }
        );
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Email = "user1@gmail.com",
                FullName = "System Admin",
                PasswordHash = "0CB47CF84CA0824A48EB7CDAD0B13AC83D6742E85A21B8A0FF58A235C2050DE9-ED1FD94795D453D2320B0A5444D4B31E",
                CreatedAt = new DateTime(2025, 12, 16, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Email = "user2@gmail.com",
                FullName = "Normal User",
                PasswordHash = "CDFCF4E8D89841B7A49EC50581EC9F5CA3AB0A93A9F23B78C69839B18BE43752-C4F0917170B9972DDE5015CBCFE31786",
                CreatedAt = new DateTime(2025, 12, 16, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Email = "user3@gmail.com",
                FullName = "Demo User",
                PasswordHash = "D3A38C51393060353567AF0865FC91B4E435AB433D177AF056F79BA1AEEADA0B-852250D8F97163710CF73F51EF6EE70D",
                CreatedAt = new DateTime(2025, 12, 16, 0, 0, 0, DateTimeKind.Utc)
            }
        );

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
