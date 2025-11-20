using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Todos;
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
            .Where(token => token.TokenId == query.TokenId && token.User_id == _userContext.UserId)
            .Select(token => new TokenResponse
            {
                TokenId = token.TokenId,
                User_id = token.User_id,
                App_id = token.App_id,
                Access_token = token.Access_token,
                Refresh_token = token.Refresh_token,
                Issued_at = token.Issued_at
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (token is null)
        {
            return Result.Failure<TokenResponse>(TokenErrors.NotFound(query.TokenId));
        }

        return token;
    }
}
