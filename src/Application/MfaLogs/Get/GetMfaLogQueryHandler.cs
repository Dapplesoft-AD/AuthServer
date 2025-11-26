using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaLogs.Get;

internal sealed class GetMfaLogQueryHandler : IQueryHandler<GetMfaLogQuery, List<MfaLogResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public GetMfaLogQueryHandler(IApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<List<MfaLogResponse>>> Handle(
        GetMfaLogQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            Guid userId = _userContext.UserId;

            if (userId == Guid.Empty)
            {
                return Result.Failure<List<MfaLogResponse>>(Error.NotFound(
                  "User.NotAuthenticated",
                  "User is not authenticated."));
            }

            List<MfaLogResponse> logs = await _context.MfaLogs
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

            return Result<List<MfaLogResponse>>.Success(logs);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<MfaLogResponse>>(Error.Failure(
                "MfaLogs.Get",
                $"Failed to retrieve MFA logs: {ex.Message}"));
        }
    }
}
