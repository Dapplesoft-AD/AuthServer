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
            .SingleOrDefaultAsync(t => t.EV_Id == command.EV_Id, cancellationToken);

        if (emailVerifications is null)
        {
            return Result.Failure(EmailVerificationErrors.NotFound(command.EV_Id));
        }

        emailVerifications.Token = command.Token;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
