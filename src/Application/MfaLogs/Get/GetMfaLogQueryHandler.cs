using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaLogs.Get;

internal sealed class GetMfaLogQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : IQueryHandler<GetMfaLogQuery, List<MfaLogResponse>>
{
    public async Task<Result<List<MfaLogResponse>>> Handle(
        GetMfaLogQuery query,
        CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        List<MfaLogResponse> logs = await context.MfaLogs
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.LoginTime) 
            .Select(x => new MfaLogResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                LoginTime = x.LoginTime,
                IpAddress = x.IpAddress,
                Device = x.Device,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return logs;
    }
}
