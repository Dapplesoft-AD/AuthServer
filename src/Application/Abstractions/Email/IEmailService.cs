
using SharedKernel;

namespace Application.Abstractions.Email;

public interface IEmailService
{
    Task<Result> SendAsync(EmailMessage message, CancellationToken ct = default);
}
