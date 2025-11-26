using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaLogs.Update;

internal sealed class UpdateMfaLogCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateMfaLogCommand>
{
    public async Task<Result> Handle(UpdateMfaLogCommand command, CancellationToken cancellationToken)
    {
        MfaLog? mfaLog = await context.MfaLogs
            .SingleOrDefaultAsync(l => l.Id == command.MfaLogId, cancellationToken);

        if (mfaLog is null)
        {
            return Result.Failure(Error.NotFound(
                "MfaLog.NotFound",
                $"MfaLog with Id {command.MfaLogId} not found."));
        }

        mfaLog.LoginTime = command.LoginTime;
        mfaLog.IpAddress = command.IpAddress;
        mfaLog.Device = command.Device;
        mfaLog.Status = command.Status;
        mfaLog.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
