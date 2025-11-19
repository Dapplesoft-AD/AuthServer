using Domain.Customers;
using Domain.Todos;
using Domain.Users;
using Domain.Application;
using Microsoft.EntityFrameworkCore;
using Domain.EmailVerification;
using Domain.PasswordResets;
using Domain.Token;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }

    DbSet<Customer> Customers { get; }
    DbSet<Applications> Applications { get; }
    DbSet<EmailVerifications> EmailVerifications { get; }
    DbSet<PasswordReset> PasswordReset { get; }
    DbSet<Tokens> Tokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
