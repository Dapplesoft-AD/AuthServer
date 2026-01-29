
using Domain.SmtpConfigs;

namespace Application.Abstractions.Email;

public interface ISmtpConfigRepository
{
    Task<SmtpConfig?> GetActiveAsync(CancellationToken ct = default);
}
