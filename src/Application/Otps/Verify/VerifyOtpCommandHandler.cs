using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Otps.Update;
using Domain.Otps;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Otps.Verify;

internal sealed class VerifyOtpCommandHandler(
    IApplicationDbContext applicationDbContext,
    ICommandHandler<UpdateOtpCommand, bool> handler) : ICommandHandler<VerifyOtpCommand, bool>
{
    public async Task<Result<bool>> Handle(
    VerifyOtpCommand request,
    CancellationToken cancellationToken)
    {
        Otp? otp = !string.IsNullOrWhiteSpace(request.Email)
            ? await applicationDbContext.Otp.FirstOrDefaultAsync(
                o => o.Email == request.Email && o.OtpToken == request.OtpToken,
                cancellationToken)
            : await applicationDbContext.Otp.FirstOrDefaultAsync(
                o => o.PhoneNumber == request.PhoneNumber && o.OtpToken == request.OtpToken,
                cancellationToken);

        if (otp is null)
        {
            return Result.Failure<bool>("OTP not found.");
        }

        if (otp.IsExpired || DateTime.UtcNow > otp.CreatedAt.Add(otp.Delay))
        {
            return Result.Failure<bool>("OTP expired.");
        }

        Result<bool> updateResult = await handler.Handle(
            new UpdateOtpCommand(otp.OtpId),
            cancellationToken);

        if (updateResult.IsFailure)
        {
            return Result.Failure<bool>("OTP verification failed while updating state.");
        }

        return Result.Success(true);
    }
}
