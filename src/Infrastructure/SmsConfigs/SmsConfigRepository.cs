using Application.Abstractions.Data;
using Application.Abstractions.SMS;
using Domain.SmsConfigs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SmsConfigs;

public class SmsConfigRepository(
    IApplicationDbContext context) : ISmsConfigRepository
{
    public async Task<SmsConfig> GetActiveProviderAsync(CancellationToken ct = default)
    {
        return await context.SmsConfig
            .AsNoTracking()
            .Where(x => x.IsActive)
            .FirstOrDefaultAsync(ct);
    }
}
