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
            .Where(emailVerification => emailVerification.Id == query.Id && emailVerification.User_Id == userContext.UserId)
            .Select(emailVerification => new EmailVerificationResponse
            {
                Id = emailVerification.Id,
                User_Id = emailVerification.User_Id,
                Token = emailVerification.Token,
                Expires_at = emailVerification.Expires_at,
                Verified_at = emailVerification.Verified_at
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (emailVerification is null)
        {
            return Result.Failure<EmailVerificationResponse>(EmailVerificationErrors.NotFound(query.Id));
        }

        return emailVerification;
    }
}
