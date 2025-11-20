using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.EmailVerification;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.EmailVerification.Delete;

internal sealed class DeleteEmailVerificationCommandHandler(IApplicationDbContext context, IUserContext userContext)
    : ICommandHandler<DeleteEmailVerificationCommand>
{
    public async Task<Result> Handle(DeleteEmailVerificationCommand command, CancellationToken cancellationToken)
    {
        EmailVerifications? emailVerifications = await context.EmailVerifications
            .SingleOrDefaultAsync(t => t.EvId == command.EvId && t.UserId == userContext.UserId, cancellationToken);

        if (emailVerifications is null)
        {
            return Result.Failure(EmailVerificationErrors.NotFound(command.EvId));
        }

        context.EmailVerifications.Remove(emailVerifications);

        emailVerifications.Raise(new EmailVerificationDeletedDomainEvent(emailVerifications.EvId));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
