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
            .Where(passwordResetResponse => passwordResetResponse.PR_Id == query.PR_Id && passwordResetResponse.User_Id == userContext.UserId)
            .Select(passwordResetResponse => new PasswordResetResponse
            {
                User_Id = passwordResetResponse.User_Id,
                Token = passwordResetResponse.Token,
                Expires_at = passwordResetResponse.Expires_at,
                Used = passwordResetResponse.Used
            }).SingleOrDefaultAsync(cancellationToken);

        if (passwordResetResponse is null)
        {
            return Result.Failure<PasswordResetResponse>(PasswordResetsErrors.NotFound(query.PR_Id));
        }

        return passwordResetResponse;
    }
}
