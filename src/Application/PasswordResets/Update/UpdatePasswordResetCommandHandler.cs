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
            .SingleOrDefaultAsync(t => t.PrId == command.PrId, cancellationToken);

        if (passwordReset is null)
        {
            return Result.Failure(PasswordResetsErrors.NotFound(command.PrId));
        }

        passwordReset.Token = command.Token;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
