using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Domain.EmailVerification;
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
            .Where(emailVerifications => emailVerifications.User_Id == query.UserId)
            .Select(emailVerifications => new EmailVerificationResponse
            {
                EV_Id = emailVerifications.EV_Id,
                User_Id = emailVerifications.User_Id,
                Token = emailVerifications.Token,
                Expires_at = emailVerifications.Expires_at,
                Verified_at = emailVerifications.Verified_at
            }).ToListAsync(cancellationToken);

        return emailVerifications;
    }
}
