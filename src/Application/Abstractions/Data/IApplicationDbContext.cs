using Domain.Customers;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Customer> Customers { get; }

    EntityEntry Entry(object entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
