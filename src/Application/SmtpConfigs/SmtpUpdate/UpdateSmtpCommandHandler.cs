using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SmtpConfigs;
using SharedKernel;

namespace Application.SmtpConfigs.SmtpUpdate;

internal sealed class UpdateSmtpCommandHandler(
    IApplicationDbContext applicationDbContext) : ICommandHandler<UpdateSmtpCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateSmtpCommand command, CancellationToken cancellationToken)
    {
        SmtpConfig smtpConfig = await applicationDbContext.SmtpConfig
            .FindAsync([command.SmtpId], cancellationToken);
        if (smtpConfig is null)
        {
            return Result.Failure<Guid>($"SMTP configuration with ID {command.SmtpId} not found.");
        }
        smtpConfig.Username = command.Username;
        smtpConfig.Password = command.Password;
        smtpConfig.SenderEmail = command.SenderEmail;
        applicationDbContext.SmtpConfig.Update(smtpConfig);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(smtpConfig.SmtpId);
    }
}
