using Domain.Applications;
using Domain.Customers;
using Domain.EmailVerification;
using Domain.PasswordResets;
using Domain.Todos;
using Domain.Token;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DomainEvents;
using SharedKernel;
using Application.Abstractions.Data;

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
