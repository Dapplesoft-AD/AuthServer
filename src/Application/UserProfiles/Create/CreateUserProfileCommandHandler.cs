using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.UserProfiles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.UserProfiles.Create;

// this is technically create and update operation

internal sealed class CreateUserProfileCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext) : ICommandHandler<CreateUserProfileCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserProfileCommand command, CancellationToken cancellationToken)
    {

        if (userContext.UserId != command.UserId)
        {
            return Result.Failure<Guid>(UserErrors.Unauthorized());
        }

        User? existingUser = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (existingUser is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
        }

        // Check if a profile exists
        UserProfile? existingProfile = await context.UserProfile
            .SingleOrDefaultAsync(up => up.UserId == command.UserId, cancellationToken);

        if (existingProfile is not null)
        {
            // Override existing profile
            existingProfile.Address = command.Address;
            existingProfile.City = command.City;
            existingProfile.Country = command.Country;
            existingProfile.PostalCode = command.PostalCode;
            existingProfile.ProfileImageUrl = command.ProfileImageUrl;
            existingProfile.DateOfBirth = command.DateOfBirth;

            context.UserProfile.Update(existingProfile);
        }
        else
        {
            // Create new profile
            var userProfile = new UserProfile
            {
                UserId = command.UserId,
                Address = command.Address,
                City = command.City,
                Country = command.Country,
                PostalCode = command.PostalCode,
                ProfileImageUrl = command.ProfileImageUrl,
                DateOfBirth = command.DateOfBirth,
            };

            await context.UserProfile.AddAsync(userProfile, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(command.UserId);
    }
}



//UserProfile? existingProfile = await context.UserProfile.AsNoTracking()  // already exist check
//    .SingleOrDefaultAsync(up => up.UserId == command.UserId, cancellationToken);

//if (existingProfile is not null)
//{
//    throw new InvalidOperationException($"UserProfile for user {command.UserId} already exists.");
//}
