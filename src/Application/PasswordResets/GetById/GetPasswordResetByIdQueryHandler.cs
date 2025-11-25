using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.PasswordResets;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.PasswordResets.GetById;

internal sealed class GetPasswordResetByIdQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetPasswordResetByIdQuery, PasswordResetResponse>
{
    public async Task<Result<PasswordResetResponse>> Handle(GetPasswordResetByIdQuery query, CancellationToken cancellationToken)
    {
        PasswordResetResponse? passwordResetResponse = await context.PasswordReset
            .Where(passwordResetResponse => passwordResetResponse.PrId == query.PrId && passwordResetResponse.UserId == userContext.UserId)
            .Select(passwordResetResponse => new PasswordResetResponse
            {
                User_Id = passwordResetResponse.UserId,
                Token = passwordResetResponse.Token,
                Expires_at = passwordResetResponse.ExpiresAt,
                Used = passwordResetResponse.Used
            }).SingleOrDefaultAsync(cancellationToken);

        if (passwordResetResponse is null)
        {
            return Result.Failure<PasswordResetResponse>(PasswordResetsErrors.NotFound(query.PrId));
        }

        return passwordResetResponse;
    }
}
