using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.EmailVerification;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.EmailVerification.GetById;

internal sealed class GetEmailVerificationByIdQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetEmailVerificationByIdQuery, EmailVerificationResponse>
{
    public async Task<Result<EmailVerificationResponse>> Handle(GetEmailVerificationByIdQuery query, CancellationToken cancellationToken)
    {
        EmailVerificationResponse? emailVerification = await context.EmailVerifications
            .Where(emailVerification => emailVerification.EvId == query.EvId && emailVerification.UserId == userContext.UserId)
            .Select(emailVerification => new EmailVerificationResponse
            {
                EvId = emailVerification.EvId,
                UserId = emailVerification.UserId,
                Token = emailVerification.Token,
                ExpiresAt = emailVerification.ExpiresAt,
                VerifiedAt = emailVerification.VerifiedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (emailVerification is null)
        {
            return Result.Failure<EmailVerificationResponse>(EmailVerificationErrors.NotFound(query.EvId));
        }

        return emailVerification;
    }
}
