using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.AuditLogs.Delete;

internal sealed class DeleteAuditLogCommandHandler
    : ICommandHandler<DeleteAuditLogCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAuditLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteAuditLogCommand command, CancellationToken cancellationToken)
    {
        AuditLog? auditLog = await _context.AuditLogs
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (auditLog is null)
        {
            return Result.Failure(AuditLogErrors.NotFound(command.Id));
        }

        _context.AuditLogs.Remove(auditLog);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
