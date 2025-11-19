using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.AuditLogs.Get;
internal sealed class GetAuditLogsQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : IQueryHandler<GetAuditLogsQuery, List<AuditLogResponse>>
{
    public async Task<Result<List<AuditLogResponse>>> Handle(
        GetAuditLogsQuery query,
        CancellationToken cancellationToken)
    {
        // Authorization check
        Guid userId = userContext.UserId;

        List<AuditLogResponse> logs = await context.AuditLogs
            .Where(x => x.UserId == userId)
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
