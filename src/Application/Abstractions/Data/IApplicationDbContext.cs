using Domain.Customers;
using Domain.Todos;
using Domain.Users;
using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using Domain.AuditLogs;
using Domain.MfaLogs;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }

    DbSet<Customer> Customers { get; }
   
    DbSet<MfaSetting> MfaSettings { get; }

    DbSet<MfaLog> MfaLogs { get; }
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
