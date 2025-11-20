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
            .SingleOrDefaultAsync(t => t.PrId == command.PrId && t.UserId == userContext.UserId, cancellationToken);

        if (passwordReset is null)
        {
            return Result.Failure(PasswordResetsErrors.NotFound(command.PrId));
        }

        context.PasswordReset.Remove(passwordReset);

        passwordReset.Raise(new PasswordResetDeletedDomainEvent(passwordReset.PrId));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
