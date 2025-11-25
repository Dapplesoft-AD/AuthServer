using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.EmailVerification.Get;

internal sealed class GetEmailVerificationQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetEmailVerificationsQuery, List<EmailVerificationResponse>>
{
    public async Task<Result<List<EmailVerificationResponse>>> Handle(GetEmailVerificationsQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<List<EmailVerificationResponse>>(UserErrors.Unauthorized());
        }

        List<EmailVerificationResponse> emailVerifications = await context.EmailVerifications
            .Where(emailVerifications => emailVerifications.UserId == query.UserId)
            .Select(emailVerifications => new EmailVerificationResponse
            {
                EvId = emailVerifications.EvId,
                UserId = emailVerifications.UserId,
                Token = emailVerifications.Token,
                ExpiresAt = emailVerifications.ExpiresAt,
                VerifiedAt = emailVerifications.VerifiedAt
            }).ToListAsync(cancellationToken);

        return emailVerifications;
    }
}
