using Application.Abstractions.Data;
using Application.Abstractions.Email;
using Domain.SmtpConfigs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SmtpConfigs;

public sealed class SmtpConfigRepository(IApplicationDbContext context) : ISmtpConfigRepository
{
    public async Task<SmtpConfig?> GetActiveAsync(CancellationToken ct = default)
    {
        return await context.SmtpConfig
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);
    }
}
