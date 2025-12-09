using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Otps;
using SharedKernel;

namespace Application.Otps.Update;

internal sealed class UpdateOtpCommandHandler(
    IApplicationDbContext applicationDbContext) : ICommandHandler<UpdateOtpCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateOtpCommand command, CancellationToken cancellationToken)
    {
        Otp? otp = await applicationDbContext.Otp
            .FindAsync([command.OtpId], cancellationToken);
        if (otp is null)
        {
            return Result.Failure<bool>("OTP is not found.");
        }
        if (otp.IsExpired)
        {
            return Result.Failure<bool>("OTP is already expired.");
        }
        if (otp.CreatedAt.Add(otp.Delay) < DateTime.UtcNow)
        {
            otp.IsExpired = true;
            applicationDbContext.Otp.Update(otp);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
            return Result.Failure<bool>("OTP is already expired.");
        }
        otp.IsExpired = true;
        applicationDbContext.Otp.Update(otp);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(true);
    }
}
