using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Otps.Create;
using Domain.Otps;
using Domain.SmsConfigs;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.SmsConfigs.OtpSms;

internal class SmsOtpCommandHandler(
    IApplicationDbContext applicationDbContext,
    ICommandHandler<CreateOtpCommand, Guid> handler,
    ILogger<SmsOtpCommandHandler> logger
) : ICommandHandler<SmsOtpCommand, Guid>
{
    private readonly ILogger<SmsOtpCommandHandler> _logger = logger;
    public async Task<Result<Guid>> Handle(SmsOtpCommand command, CancellationToken cancellationToken)
    {
        SmsConfig? smsConfig = await applicationDbContext.SmsConfig
            .FirstOrDefaultAsync(cancellationToken);

        if (smsConfig is null)
        {
            return Result.Failure<Guid>(SmsConfigErrors.NotFound(Guid.Empty));
        }

        var createOtp = new CreateOtpCommand(null, command.PhoneNumber);
        Result<Guid> otpResult = await handler.Handle(createOtp, cancellationToken);

        if (otpResult.IsFailure)
        {
            return Result.Failure<Guid>("OTP generation failed.");
        }

        Guid otpId = otpResult.Value;

        Otp? otp = await applicationDbContext.Otp
            .FirstOrDefaultAsync(t => t.OtpId == otpId, cancellationToken);

        if (otp is null)
        {
            return Result.Failure<Guid>("OTP not found.");
        }

        string message = $"Your OTP is {otp.OtpToken}. It will expire in 3 minutes.";

        string responseString;

        try
        {
            responseString = await "https://api.bdbulksms.net/api.php?"
                .PostUrlEncodedAsync(new
                {
                    token = smsConfig.SmsToken,
                    phone = otp.PhoneNumber,
                    message
                }, cancellationToken: cancellationToken)
                .ReceiveString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SMS sending exception");
            return Result.Failure<Guid>($"SMS sending exception: {ex.Message}");
        }

        // Safe OK check
        if (responseString.Trim().StartsWith("OK", StringComparison.OrdinalIgnoreCase))
        {
            return Result.Success(otpId);
        }

        _logger.LogWarning("SMS sending failed. Response: {Response}", responseString);
        return Result.Failure<Guid>($"SMS sending failed: {responseString}");
    }
}
