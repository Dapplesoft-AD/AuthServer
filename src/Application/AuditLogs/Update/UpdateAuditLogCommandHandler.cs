using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.AuditLogs.Update;

internal sealed class UpdateAuditLogCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<UpdateAuditLogCommand>
{
    public async Task<Result> Handle(UpdateAuditLogCommand command, CancellationToken cancellationToken)
    {
        AuditLog? auditLog = await context.AuditLogs
            .SingleOrDefaultAsync(a => a.Id == command.AuditLogId, cancellationToken);

        if (auditLog is null)
        {
            return Result.Failure(Error.NotFound(
                "AuditLog.NotFound",
                $"AuditLog with Id {command.AuditLogId} not found."));
        }

        
        auditLog.Action = command.Action;
        auditLog.Description = command.Description;
        auditLog.UpdatedAt = DateTime.UtcNow;  

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
