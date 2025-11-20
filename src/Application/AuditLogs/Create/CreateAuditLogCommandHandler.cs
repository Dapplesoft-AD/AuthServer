using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.AuditLogs;
using SharedKernel;

namespace Application.AuditLogs.Create;
public class CreateAuditLogCommandHandler : ICommandHandler<CreateAuditLogCommand, Guid>
{
    private readonly IApplicationDbContext context;

    public CreateAuditLogCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<Guid>> Handle(CreateAuditLogCommand command, CancellationToken cancellationToken)
    {
        var auditLog = new AuditLog
        {
            UserId = command.UserId,
            BusinessId = command.BusinessId,
            Action = command.Action,
            Description = command.Description,
            CreatedAt = command.CreatedAt
        };

        await context.AuditLogs.AddAsync(auditLog, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(auditLog.Id);
    }
}
