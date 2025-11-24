using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.UserLoginHistories.GetByUserId;

internal sealed class GetUserLoginHistoryByUserIdQueryHandler(
    IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetUserLoginHistoryByUserIdQuery, List<UserLoginHistoryResponse>>
{
    public async Task<Result<List<UserLoginHistoryResponse>>> Handle(GetUserLoginHistoryByUserIdQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<List<UserLoginHistoryResponse>>(UserErrors.Unauthorized());
        }

        List<UserLoginHistoryResponse> HistoryEntities = await context.UserLoginHistory
            .Where(h => h.UserId == query.UserId)
            .Select(entity => new UserLoginHistoryResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                IpAddress = entity.IpAddress,
                Country = entity.Country,
                City = entity.City,
                Browser = entity.Browser,
                OS = entity.OS,
                Device = entity.Device,
                LogInTime = entity.LogInTime,
                LogoutTime = entity.LogoutTime,
                Status = entity.Status,
            })
            .ToListAsync(cancellationToken);

        return HistoryEntities;
    }
}
