using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaSettings.Update;

internal sealed class UpdateMfaSettingCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<UpdateMfaSettingCommand>
{
    public async Task<Result> Handle(UpdateMfaSettingCommand command, CancellationToken cancellationToken)
    {
        MfaSetting? mfa = await context.MfaSettings
            .SingleOrDefaultAsync(a => a.Id == command.MfaSettingId, cancellationToken);

        if (mfa is null)
        {
            return Result.Failure(Error.NotFound(
                "MfaSetting.NotFound",
                $"MfaSetting with Id {command.MfaSettingId} not found."));
        }

        // Method parse-safe
        bool isParsed = Enum.TryParse<MfaMethod>(command.Method, true, out MfaMethod parsedMethod);
        if (!isParsed)
        {
            return Result.Failure(
                    Error.Failure(
           "MfaSetting.InvalidMethod",
           $"Invalid MFA method: {command.Method}"
                  ));
        }

        // Update fields
        mfa.UserId = command.UserId;
        mfa.SecretKey = command.SecretKey ?? mfa.SecretKey;
        mfa.BackupCodes = command.BackupCodes ?? mfa.BackupCodes;
        mfa.Method = parsedMethod;
        mfa.Enabled = command.Enabled;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
