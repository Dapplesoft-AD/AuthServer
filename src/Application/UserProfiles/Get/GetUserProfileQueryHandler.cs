using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.UserProfiles.Create;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.UserProfiles.Get;
internal sealed class GetUserProfileQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : IQueryHandler<GetUserProfileQuery, UserProfileResponse>
{
    public async Task<Result<UserProfileResponse>> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<UserProfileResponse>(UserErrors.Unauthorized());
        }

        UserProfileResponse? userProfile = await context.UserProfile
            .Where(up => up.UserId == query.UserId)
            .Select(up => new UserProfileResponse
            {
                UserId = up.UserId,
                Address = up.Address,
                City = up.City,
                Country = up.Country,
                PostalCode = up.PostalCode,
                ProfileImageUrl = up.ProfileImageUrl,
                DateOfBirth = up.DateOfBirth
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (userProfile is null)
        {
            return Result.Failure<UserProfileResponse>(UserErrors.NotFound(query.UserId));
        }

        return Result.Success(userProfile);
    }
}
