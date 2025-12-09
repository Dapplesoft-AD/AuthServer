using System.Globalization;
using System.Security.Cryptography;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Otps;
using SharedKernel;

namespace Application.Otps.Create;

internal sealed class CreateOtpCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateOtpCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOtpCommand command, CancellationToken cancellationToken)
    {

        var otp = new Otp
        {
            OtpToken = GenerateOtp(),
            Email = command.Email,
            CreatedAt = dateTimeProvider.UtcNow,
            IsExpired = false
        };

        otp.Raise(new OtpCreatedDomainEvent(otp.OtpId));

        context.Otp.Add(otp);

        await context.SaveChangesAsync(cancellationToken);

        return otp.OtpId;
    }
    public static string GenerateOtp()
    {
        int otp = RandomNumberGenerator.GetInt32(0, 10_000); // produces 0..9999
        return otp.ToString("D4", CultureInfo.InvariantCulture);
    }
}
