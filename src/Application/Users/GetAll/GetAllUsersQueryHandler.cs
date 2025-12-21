using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.GetAll;

internal sealed class GetAllUsersQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetAllUsersQuery, List<GetAllUsersQueryResponse>>
{
    public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        // Admin role condition will be applied
        List<GetAllUsersQueryResponse> users = await context.Users
            .AsNoTracking()
            .Select(user => new GetAllUsersQueryResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.Phone,
                IsEmailVerified = user.IsEmailVerified,
                IsMFAEnabled = user.IsMFAEnabled,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            })
            .ToListAsync(cancellationToken: cancellationToken);

        return Result<Result<List<GetAllUsersQueryResponse>>>.Success(users);
    }
}
