using SharedKernel;

namespace Domain.Otps;

public static class OtpErrors
{
    public static Error AlreadyCreated(Guid OtpId) => Error.Problem(
        "Otp.AlreadyCreated",
        $"The Otp with Id = '{OtpId}' is already created.");
    public static Error NotFound(Guid OtpId) => Error.NotFound(
        "Otp.NotFound",
        $"The Otp with the Id = '{OtpId}' was not found");
}
