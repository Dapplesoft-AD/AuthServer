using Domain.Customers;
using Domain.Todos;
using Domain.UserLoginHistories;
using Domain.UserProfiles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }

    DbSet<UserLoginHistory> UserLoginHistory { get; set; }

    DbSet<UserProfile> UserProfile { get; set; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Customer> Customers { get; }

    EntityEntry Entry(object entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
