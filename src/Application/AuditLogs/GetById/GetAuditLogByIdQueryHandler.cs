using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.AuditLogs.GetById;

internal sealed class GetAuditLogByIdQueryHandler
    : IQueryHandler<GetAuditLogByIdQuery, AuditLogResponse>
{
    private readonly IApplicationDbContext _context;

    public GetAuditLogByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<AuditLogResponse>> Handle(
        GetAuditLogByIdQuery query,
        CancellationToken cancellationToken)
    {
        AuditLog? auditLog = await _context.AuditLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (auditLog is null)
        {
            return Result.Failure<AuditLogResponse>(AuditLogErrors.NotFound(query.Id));
        }

        var response = new AuditLogResponse
        {
            Id = auditLog.Id,
            UserId = auditLog.UserId,
            BusinessId = auditLog.BusinessId,
            Action = auditLog.Action,
            Description = auditLog.Description,
            CreatedAt = auditLog.CreatedAt,
            UpdatedAt = auditLog.UpdatedAt
        };

        return response;
    }
}
