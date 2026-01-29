using Domain.Otps;
using SharedKernel;

namespace Application.Abstractions.Otps;

public interface IOtpProviderService
{
    Task<Result<Guid>> SendOtpAsync(
        string destination,
        OtpType? otpType = OtpType.Default,
        CancellationToken cancellationToken = default);

    Task<Result> VerifyOtpAsync(
        string destination,
        string otpToken,
        OtpType? otpType = OtpType.Default,
        CancellationToken cancellationToken = default);
}
