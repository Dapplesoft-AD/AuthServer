
using SharedKernel;

namespace Application.Abstractions.SMS;

public interface ISmsService
{
    Task<Result> SendOtpAsync(string phoneNumber, string message, CancellationToken ct);
}
