using Application.Abstractions.Messaging;
using Application.Abstractions.Otps;
using Domain.Otps;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.SmsConfigs.OtpSms;

internal class SmsOtpCommandHandler(
    IOtpProviderService otpProviderService,
    ILogger<SmsOtpCommandHandler> logger
) : ICommandHandler<SmsOtpCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        SmsOtpCommand command,
        CancellationToken cancellationToken
    )
    {
        Result<Guid> otpResult = await otpProviderService.SendOtpAsync(
            command.PhoneNumber,
            OtpType.Default,
            cancellationToken
        );

        if (otpResult.IsFailure)
        {
            logger.LogWarning("SMS failed: {Error}", otpResult.Error);

            return Result.Failure<Guid>(otpResult.Error);
        }

        return Result.Success(otpResult.Value);
    }
}
