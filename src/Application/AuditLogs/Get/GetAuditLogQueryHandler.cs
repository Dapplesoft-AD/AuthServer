using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.AuditLogs;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.AuditLogs.Get;
internal sealed class GetAuditLogQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : IQueryHandler<GetAuditLogQuery, List<AuditLogResponse>>
{
    public async Task<Result<List<AuditLogResponse>>> Handle(
     GetAuditLogQuery query,
     CancellationToken cancellationToken)
    {
        Guid? userId = userContext.UserId;

        IQueryable<AuditLog> auditQuery = context.AuditLogs;

        // If user is logged in → filter their logs  
        // If user is anonymous → return ALL logs
        if (userId.HasValue && userId.Value != Guid.Empty)
        {
            //auditQuery = auditQuery.Where(x => x.UserId == userId);
        }

        List<AuditLogResponse> logs = await auditQuery
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new AuditLogResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                BusinessId = x.BusinessId,
                Action = x.Action,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return logs;
    }

}
