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
    private readonly IUserContext _userContext;
    public GetTokensQueryHandler(IApplicationDbContext applicationDbContext, IUserContext userContext)
    {
        _context = applicationDbContext;
        _userContext = userContext;
    }

    public async Task<Result<List<TokenResponse>>> Handle(GetTokensQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != _userContext.UserId)
        {
            return Result.Failure<List<TokenResponse>>(UserErrors.Unauthorized());
        }
        List<TokenResponse> tokens = await _context.Tokens
            .Where(tokens => tokens.UserId == query.UserId)
            .Select(tokens => new TokenResponse
            {
                TokenId = tokens.TokenId,
                UserId = tokens.UserId,
                AppId = tokens.AppId,
                Accesstoken = tokens.Accesstoken,
                Refreshtoken = tokens.Refreshtoken,
                IssuedAt = tokens.IssuedAt
            }).ToListAsync(cancellationToken);

        return tokens;
    }
}
