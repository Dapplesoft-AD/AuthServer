using System;
using System.Globalization;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Otps.Create;
using Domain.Otps;
using Domain.SmsConfigs;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SmsConfigs.OtpSms;

internal class SmsOtpCommandHandler(
    IApplicationDbContext applicationDbContext,
    ICommandHandler<CreateOtpCommand, Guid> handler
) : ICommandHandler<SmsOtpCommand, Guid>
{
    public async Task<Result<Guid>> Handle(SmsOtpCommand command, CancellationToken cancellationToken)
    {
        SmsConfig? smsConfig = await applicationDbContext.SmsConfig
            .FirstOrDefaultAsync(cancellationToken);

        if (smsConfig is null)
        {
            return Result.Failure<Guid>("Sms configuration not found.");
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

        string message = $"Your OTP is {otp.OtpToken}. It will expire in 5 minutes.";

        string responseString = await "https://api.bdbulksms.net/api.php?"
            .PostUrlEncodedAsync(new
            {
                token = smsConfig.SmsToken,
                phone = otp.PhoneNumber,
                message
            }, cancellationToken: cancellationToken)
            .ReceiveString();

        if (responseString.Contains("OK", StringComparison.OrdinalIgnoreCase))
        {
            return Result.Success(otpId); // return OTP ID (correct!)
        }

        return Result.Failure<Guid>($"SMS sending failed: {responseString}");
    }
}
