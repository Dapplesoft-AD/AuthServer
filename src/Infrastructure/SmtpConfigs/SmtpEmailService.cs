using System.Net;
using System.Net.Mail;
using Application.Abstractions.Email;
using Domain.SmtpConfigs;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Infrastructure.SmtpConfigs;

public sealed class SmtpEmailService(
    ISmtpConfigRepository configRepository,
    ILogger<SmtpEmailService> logger
) : IEmailService
{
    public async Task<Result> SendAsync(EmailMessage message, CancellationToken ct = default)
    {
        SmtpConfig? config = await configRepository.GetActiveAsync(ct);

        if (config is null)
        {
            logger.LogWarning("No active SMTP config found.");
            //throw new InvalidOperationException("No SMTP config available");
            return Result.Failure(Error.NotFound("SMTP Service: 404","No SMTP config available"));
        }

        try
        {
            using var smtp = new SmtpClient(config.Host, config.Port)
            {
                Credentials = new NetworkCredential(config.Username, config.Password),
                EnableSsl = config.EnableSsl,
            };

            using var mail = new MailMessage
            {
                From = new MailAddress(config.SenderEmail),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = message.IsHtml,
            };

            mail.To.Add(message.To);

            await smtp.SendMailAsync(mail, ct);

            logger.LogInformation("Email sent successfully to {To}", message.To);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {To}", message.To);
            throw new Exception();
        }

        return Result.Success();
    }
}
