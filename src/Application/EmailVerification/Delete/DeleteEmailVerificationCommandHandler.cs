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
            .SingleOrDefaultAsync(t => t.EV_Id == command.EV_Id && t.User_Id == userContext.UserId, cancellationToken);

        if (emailVerifications is null)
        {
            return Result.Failure(EmailVerificationErrors.NotFound(command.EV_Id));
        }

        context.EmailVerifications.Remove(emailVerifications);

        emailVerifications.Raise(new EmailVerificationDeletedDomainEvent(emailVerifications.EV_Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
