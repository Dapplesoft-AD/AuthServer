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

        var passwordReset = new PasswordReset
        {
            User_Id = command.User_Id,
            Token = command.Token,
            Expires_at = command.Expires_at == default
                ? dateTimeProvider.UtcNow
                : command.Expires_at,
            Used = command.Used
        };

        passwordReset.Raise(new PasswordResetCreatedDomainEvent(passwordReset.Id));

        context.PasswordReset.Add(passwordReset);

        await context.SaveChangesAsync(cancellationToken);

        return passwordReset.Id;
    }
}
