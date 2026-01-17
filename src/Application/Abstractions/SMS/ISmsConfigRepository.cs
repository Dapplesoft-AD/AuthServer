using Domain.SmsConfigs;

namespace Application.Abstractions.SMS;

public interface ISmsConfigRepository
{
    Task<SmsConfig> GetActiveProviderAsync(CancellationToken ct = default);
}
