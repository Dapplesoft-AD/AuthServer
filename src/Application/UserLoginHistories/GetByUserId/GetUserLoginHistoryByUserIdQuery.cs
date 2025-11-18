using Application.Abstractions.Messaging;

namespace Application.UserLoginHistories.GetByUserId;

public sealed record GetUserLoginHistoryByUserIdQuery : IQuery<List<UserLoginHistoryResponse>>
{
    public Guid UserId { get; }
    public GetUserLoginHistoryByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }
}
