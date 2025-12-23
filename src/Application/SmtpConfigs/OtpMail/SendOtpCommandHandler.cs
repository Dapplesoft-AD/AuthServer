using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Otps.Create;
using Application.SmsConfigs.OtpSms;
using Domain.Otps;
using Domain.SmtpConfigs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using SharedKernel;

namespace Application.SmtpConfigs.OtpMail;

internal sealed class SendOtpCommandHandler(
    IApplicationDbContext context,
    ICommandHandler<CreateOtpCommand, Guid> handler,
    ILogger<SmsOtpCommandHandler> logger)
    : ICommandHandler<SendOtpCommand, Guid>
{
    private readonly ILogger<SmsOtpCommandHandler> _logger = logger;
    public async Task<Result<Guid>> Handle(SendOtpCommand command, CancellationToken cancellationToken)
    {
        List<SmtpConfig>? smtpConfig = await context.SmtpConfig.ToListAsync(cancellationToken);

        if (smtpConfig.Count==0)
        {
            return Result.Failure<Guid>("SMTP configuration not found.");
        }

        var createOtpCommand = new CreateOtpCommand(command.RecipientEmail,null);
        Result<Guid> otpResult = await handler.Handle(createOtpCommand, cancellationToken);

        if (otpResult.IsFailure)
        {
            return Result.Failure<Guid>("OTP generation failed.");
        }

        Guid otpId = otpResult.Value;
        Otp? otp = await context.Otp.FirstOrDefaultAsync(t => t.OtpId == otpId, cancellationToken);
        if (otp is null)
        {
            return Result.Failure<Guid>("OTP not found");
        }
        foreach(SmtpConfig config in smtpConfig)
        {
            using var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.SenderEmail, config.SenderEmail));
            message.To.Add(new MailboxAddress(command.RecipientEmail, command.RecipientEmail));
            message.Subject = "OTP Verification Mail";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"Your OTP is: {otp.OtpToken}"
            };
            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using var client = new SmtpClient();
                SecureSocketOptions sslOption = config.EnableSsl
                    ? SecureSocketOptions.StartTls
                    : SecureSocketOptions.None;

                await client.ConnectAsync(
                    config.Host,
                    config.Port,
                    sslOption,
                    cancellationToken);

                await client.AuthenticateAsync(
                    config.Username,
                    config.Password,
                    cancellationToken);

                await client.SendAsync(message, cancellationToken);

                await client.DisconnectAsync(true, cancellationToken);

                return Result.Success(otpId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   "SMS sending failed for provider {ProviderId}", config.SmtpId);
            }
        }
        return Result.Failure<Guid>("No SMTP configuration available to send email.");
    }
}
