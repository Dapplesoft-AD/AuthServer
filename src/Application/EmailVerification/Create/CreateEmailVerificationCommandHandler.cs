using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.EmailVerification;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.EmailVerification.Create;

internal sealed class CreateEmailVerificationCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext)
    : ICommandHandler<CreateEmailVerificationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEmailVerificationCommand command, CancellationToken cancellationToken)
    {
        if (userContext.UserId != command.User_Id)
        {
            return Result.Failure<Guid>(UserErrors.Unauthorized());
        }

        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.User_Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(command.User_Id));
        }

        var emailVerifications = new EmailVerifications
        {
            User_Id = command.User_Id,
            Token = command.Token,
            Expires_at = command.Expires_at == default
                ? dateTimeProvider.UtcNow
                : command.Expires_at,
            Verified_at = command.Verified_at == default
                ? dateTimeProvider.UtcNow
                : command.Verified_at
        };

        emailVerifications.Raise(new EmailVerificationCreatedDomainEvent(emailVerifications.Id));

        context.EmailVerifications.Add(emailVerifications);

        await context.SaveChangesAsync(cancellationToken);

        return emailVerifications.Id;
    }
}
