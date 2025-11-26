using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaSettings;
using SharedKernel;

namespace Application.MfaSettings.Create;

internal sealed class CreateMfaSettingCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateMfaSettingCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMfaSettingCommand command, CancellationToken cancellationToken)
    {
        var mfaSetting = new MfaSetting
        {
            UserId = command.UserId,
            SecretKey = command.SecretKey,
            BackupCodes = command.BackupCodes,
            Method = command.Method,
            Enabled = command.Enabled
        };

        await context.MfaSettings.AddAsync(mfaSetting, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(mfaSetting.Id);
    }
}
