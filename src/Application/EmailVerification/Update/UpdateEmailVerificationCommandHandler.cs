using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.EmailVerification;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.EmailVerification.Update;

internal sealed class UpdateEmailVerificationCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<UpdateEmailVerificationCommand>
{
    public async Task<Result> Handle(UpdateEmailVerificationCommand command, CancellationToken cancellationToken)
    {
        EmailVerifications? emailVerifications = await context.EmailVerifications
            .SingleOrDefaultAsync(t => t.EvId == command.EvId, cancellationToken);

        if (emailVerifications is null)
        {
            return Result.Failure(EmailVerificationErrors.NotFound(command.EvId));
        }

        emailVerifications.Token = command.Token;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
