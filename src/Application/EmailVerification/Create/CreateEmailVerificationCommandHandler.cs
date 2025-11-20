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
        if (userContext.UserId != command.UserId)
        {
            return Result.Failure<Guid>(UserErrors.Unauthorized());
        }

        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
        }

        var emailVerifications = new EmailVerifications
        {
            UserId = command.UserId,
            Token = command.Token,
            ExpiresAt = command.ExpiresAt == default
                ? dateTimeProvider.UtcNow
                : command.ExpiresAt,
            VerifiedAt = command.VerifiedAt == default
                ? dateTimeProvider.UtcNow
                : command.VerifiedAt
        };

        emailVerifications.Raise(new EmailVerificationCreatedDomainEvent(emailVerifications.EvId));

        context.EmailVerifications.Add(emailVerifications);

        await context.SaveChangesAsync(cancellationToken);

        return emailVerifications.EvId;
    }
}
