using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PasswordResets.Get;

internal sealed class GetPasswordResetQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetPasswordResetQuery, List<PasswordResetResponse>>
{
    public async Task<Result<List<PasswordResetResponse>>> Handle(GetPasswordResetQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<List<PasswordResetResponse>>(UserErrors.Unauthorized());
        }

        List<PasswordResetResponse> passwordResets = await context.PasswordReset
            .Where(passwordReset => passwordReset.UserId == query.UserId)
            .Select(passwordReset => new PasswordResetResponse
            {
                PrId = passwordReset.PrId,
                User_Id = passwordReset.UserId,
                Token = passwordReset.Token,
                Expires_at = passwordReset.ExpiresAt,
                Used = passwordReset.Used

            }).ToListAsync(cancellationToken);

        return passwordResets;
    }
}
