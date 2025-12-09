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
    public async Task<Result<bool>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        Otp otp = await applicationDbContext.Otp
            .FirstOrDefaultAsync(o => o.Email == request.Email && o.OtpToken == request.OtpToken, cancellationToken);
        if (otp is null)
        {
            return Result.Failure<bool>("OTP not found.");
        }
        if (otp.IsExpired || DateTime.UtcNow > otp.CreatedAt.Add(otp.Delay))
        {
            otp.IsExpired = true;
            return Result.Failure<bool>("OTP expired.");
        }
        otp.IsExpired = true;
        var command = new UpdateOtpCommand(otp.OtpId);
        await handler.Handle(command, cancellationToken);
        return Result.Success(true);
    }
}
