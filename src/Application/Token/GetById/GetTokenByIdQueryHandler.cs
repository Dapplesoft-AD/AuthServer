using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Token;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Token.GetById;

internal class GetTokenByIdQueryHandler : IQueryHandler<GetTokenByIdQuery, TokenResponse>
{
    private readonly IUserContext _userContext;
    private readonly IApplicationDbContext _context;
    public GetTokenByIdQueryHandler(IUserContext userContext, IApplicationDbContext context)
    {
        _userContext = userContext;
        _context = context;
    }
    public async Task<Result<TokenResponse>> Handle(GetTokenByIdQuery query, CancellationToken cancellationToken)
    {
        TokenResponse? token = await _context.Tokens
            .Where(token => token.TokenId == query.TokenId && token.UserId == _userContext.UserId)
            .Select(token => new TokenResponse
            {
                TokenId = token.TokenId,
                UserId = token.UserId,
                AppId = token.AppId,
                Accesstoken = token.Accesstoken,
                Refreshtoken = token.Refreshtoken,
                IssuedAt = token.IssuedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (token is null)
        {
            return Result.Failure<TokenResponse>(TokenErrors.NotFound(query.TokenId));
        }

        return token;
    }
}
