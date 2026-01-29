using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Users.GetByEmail;

internal sealed class GetUserByEmailQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        string? emailLower = query.Email.ToLower(CultureInfo.CurrentCulture);
        UserResponse? user = await context.Users
            .Where(u => u.Email == emailLower)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                IsEmailVerified = u.IsVerified,
                IsMFAEnabled = u.IsMFAEnabled,
                Phone = u.Phone,
                CountryId = u.CountryId,
                RegionId = u.RegionId,
                DistrictId = u.DistrictId,
                SubDistrictId = u.SubDistrictId,
                Address = u.Address,
                Status = u.Status,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFoundByEmail);
        }

        if (user.Id != userContext.UserId)
        {
            return Result.Failure<UserResponse>(UserErrors.Unauthorized());
        }

        return user;
    }
}
