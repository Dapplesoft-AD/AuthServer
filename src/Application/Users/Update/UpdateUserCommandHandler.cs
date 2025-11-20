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
    IDateTimeProvider dateTimeProvider,
    IPasswordHasher passwordHasher
    ) : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (command.UserId != userContext.UserId)
        {
            return Result.Failure<UpdateUserCommand>(UserErrors.Unauthorized());
        }

        User? userTuple = await context.Users
            .SingleOrDefaultAsync(t => t.Id == command.UserId, cancellationToken);

        if (userTuple is null)
        {
            return Result.Failure(UserErrors.NotFound(command.UserId));
        }


        if (command.Email is not null)
        {
            // Check if the new email is already used by another user
            bool emailExists = await context.Users
                .AnyAsync(u => u.Email == command.Email && u.Id != command.UserId, cancellationToken);
            if (emailExists)
            {
                return Result.Failure(UserErrors.EmailNotUnique);
            }
            userTuple.Email = command.Email;
        }

        if (command.Fullname is not null)
        {
            userTuple.FullName = command.Fullname;
        }

        if (command.Password is not null)
        {
            userTuple.PasswordHash = passwordHasher.Hash(command.Password);
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

        if (context.Entry(userTuple).State == EntityState.Modified)
        {
            userTuple.UpdatedAt = dateTimeProvider.UtcNow;
            await context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
