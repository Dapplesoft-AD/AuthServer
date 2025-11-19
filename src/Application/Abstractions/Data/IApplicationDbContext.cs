using Domain.Businesses;
using Domain.BusinessMembers;
using Domain.Customers;
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
    DbSet<Business> Businesses { get; }
    DbSet<BusinessMember> BusinessMembers { get; }
    DbSet<Role> Roles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
