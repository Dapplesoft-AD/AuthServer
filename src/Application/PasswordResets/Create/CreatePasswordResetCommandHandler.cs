using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PasswordResets;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PasswordResets.Create;

internal sealed class CreatePasswordResetCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext)
    : ICommandHandler<CreatePasswordResetCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePasswordResetCommand command, CancellationToken cancellationToken)
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

        var passwordReset = new PasswordReset
        {
            UserId = command.UserId,
            Token = command.Token,
            ExpiresAt = command.ExpiresAt == default
                ? dateTimeProvider.UtcNow
                : command.ExpiresAt,
            Used = command.Used
        };

        passwordReset.Raise(new PasswordResetCreatedDomainEvent(passwordReset.PrId));

        context.PasswordReset.Add(passwordReset);

        await context.SaveChangesAsync(cancellationToken);

        return passwordReset.PrId;
    }
}
