using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Domain.EmailVerification;
using Domain.PasswordResets;
using Domain.Token;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<EmailVerifications> EmailVerifications { get; }
    DbSet<PasswordReset> PasswordReset { get; }
    DbSet<Tokens> Tokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
