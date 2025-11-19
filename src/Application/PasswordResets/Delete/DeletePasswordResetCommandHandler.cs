using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PasswordResets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PasswordResets.Delete;

internal sealed class DeletePasswordResetCommandHandler(IApplicationDbContext context, IUserContext userContext)
    : ICommandHandler<DeletePasswordResetCommand>
{
    public async Task<Result> Handle(DeletePasswordResetCommand command, CancellationToken cancellationToken)
    {
        PasswordReset? passwordReset = await context.PasswordReset
            .SingleOrDefaultAsync(t => t.Id == command.Id && t.User_Id == userContext.UserId, cancellationToken);

        if (passwordReset is null)
        {
            return Result.Failure(PasswordResetsErrors.NotFound(command.Id));
        }

        context.PasswordReset.Remove(passwordReset);

        passwordReset.Raise(new PasswordResetCreatedDomainEvent(passwordReset.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
