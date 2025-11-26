using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaLogs.Update;

internal sealed class UpdateMfaLogCommandHandler : ICommandHandler<UpdateMfaLogCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMfaLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateMfaLogCommand command, CancellationToken cancellationToken)
    {
        try
        {
            MfaLog? mfaLog = await _context.MfaLogs
                .SingleOrDefaultAsync(l => l.Id == command.MfaLogId, cancellationToken);

            if (mfaLog is null)
            {
                return Result.Failure(Error.NotFound(
                    "MfaLog.NotFound",
                    $"MfaLog with Id {command.MfaLogId} not found."));
            }

            // Update fields
            mfaLog.LoginTime = command.LoginTime;
            mfaLog.IpAddress = command.IpAddress;
            mfaLog.Device = command.Device;
            mfaLog.Status = command.Status;
            mfaLog.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Failure(
                "MfaLog.Update",
                $"Failed to update MFA log: {ex.Message}"));
        }
    }
}
