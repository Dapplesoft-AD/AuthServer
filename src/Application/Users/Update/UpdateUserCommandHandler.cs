using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Update;

internal sealed class UpdateUserCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext,
    IDateTimeProvider dateTimeProvider
    ) : ICommandHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        string emailLower = command.Email?.ToLower(CultureInfo.CurrentCulture);

        if (command.UserId != userContext.UserId)
        {
            return (Result<UpdateUserResponse>)Result.Failure(UserErrors.Unauthorized());
        }

        User? userTuple = await context.Users
            .SingleOrDefaultAsync(t => t.Id == command.UserId, cancellationToken);

        if (userTuple is null)
        {
            return (Result<UpdateUserResponse>)Result.Failure(UserErrors.NotFound(command.UserId));
        }

        if (emailLower is not null)
        {
            // Check if the new email is already used by another user
            bool emailExists = await context.Users
                .AnyAsync(u => u.Email == emailLower && u.Id != command.UserId, cancellationToken);
            if (emailExists)
            {
                return (Result<UpdateUserResponse>)Result.Failure(UserErrors.EmailNotUnique);
            }
            userTuple.Email = emailLower;
        }

        if (command.Fullname is not null)
        {
            userTuple.FullName = command.Fullname;
        }

        if (command.Phone is not null)
        {
            userTuple.Phone = command.Phone;
        }

        if (command.Status.HasValue)
        {
            userTuple.Status = command.Status.Value;
        }

        if (command.IsMFAEnabled.HasValue)
        {
            userTuple.IsMFAEnabled = command.IsMFAEnabled.Value;
        }

        if (command.IsEmailVerified.HasValue)
        {
            userTuple.IsEmailVerified = command.IsEmailVerified.Value;
        }

        if (command.CountryId.HasValue)
        {
            userTuple.CountryId = command.CountryId.Value;
        }

        if (command.RegionId.HasValue)
        {
            userTuple.RegionId = command.RegionId.Value;
        }

        if (command.DistrictId.HasValue)
        {
            userTuple.DistrictId = command.DistrictId.Value;
        }

        if (command.SubDistrictId.HasValue)
        {
            userTuple.SubDistrictId = command.SubDistrictId.Value;
        }

        // checking if changed
        if (context.Entry(userTuple).State == EntityState.Modified)
        {
            userTuple.UpdatedAt = dateTimeProvider.UtcNow;
            await context.SaveChangesAsync(cancellationToken);
        }

        var response = new UpdateUserResponse
        (
            Fullname: userTuple.FullName,
            Email: userTuple.Email,
            Phone: userTuple.Phone,
            Status: userTuple.Status,
            IsMFAEnabled: userTuple.IsMFAEnabled,
            IsEmailVerified: userTuple.IsEmailVerified
        );

        return response;
    }
}
