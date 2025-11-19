using System;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Token.Get;

internal sealed class GetTokensQueryHandler : IQueryHandler<GetTokensQuery, List<TokenResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext userContext;
    public GetTokensQueryHandler(IApplicationDbContext applicationDbContext, IUserContext userContext)
    {
        _context = applicationDbContext;
        this.userContext = userContext;
    }

    public async Task<Result<List<TokenResponse>>> Handle(GetTokensQuery query, CancellationToken cancellationToken)
    {
        if(query.User_id != userContext.UserId)
        {
            return Result.Failure<List<TokenResponse>>(UserErrors.Unauthorized());
        }
        List<TokenResponse> tokens = await _context.Tokens
            .Where(tokens => tokens.User_id == query.User_id)
            .Select(tokens => new TokenResponse
            {
                Id = tokens.Id,
                User_id = tokens.User_id,
                App_id = tokens.App_id,
                Acess_token = tokens.Acess_token,
                Refresh_token = tokens.Refresh_token,
                Issued_at = tokens.Issued_at
            }).ToListAsync(cancellationToken);

        return tokens;
    }
}
