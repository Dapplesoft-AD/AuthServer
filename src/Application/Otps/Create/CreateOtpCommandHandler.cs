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

        if (string.IsNullOrWhiteSpace(command.Email) && string.IsNullOrWhiteSpace(command.PhoneNumber))
        {
            return Result.Failure<Guid>("Either Email or PhoneNumber must be provided.");
        }
        var otp = new Otp
        {
            OtpToken = GenerateOtp(),
            CreatedAt = dateTimeProvider.UtcNow,
            IsExpired = false
        };
        if (!string.IsNullOrWhiteSpace(command.Email))
        {
            otp.Email = command.Email;
        }
        else
        {
            otp.PhoneNumber = command.PhoneNumber;
        }

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
