using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PasswordResets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PasswordResets.Update;

internal sealed class UpdatePasswordResetCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<UpdatePasswordResetCommand>
{
    public async Task<Result> Handle(UpdatePasswordResetCommand command, CancellationToken cancellationToken)
    {
        PasswordReset? passwordReset = await context.PasswordReset
            .SingleOrDefaultAsync(t => t.Id == command.Id, cancellationToken);

        if (passwordReset is null)
        {
            return Result.Failure(PasswordResetsErrors.NotFound(command.Id));
        }

        passwordReset.Token = command.Token;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
