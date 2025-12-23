using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Openiddict;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Models;

namespace Application.Tokens.Get;

internal sealed class GetTokensQueryHandler(
    IOpeniddictTokenService tokenService) : IQueryHandler<GetTokensQuery, IReadOnlyList<Token>>
{
    public async Task<Result<IReadOnlyList<Token>>> Handle(GetTokensQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<Token> tokens = await tokenService.GetAllAsync(cancellationToken);
            return Result.Success(tokens);
        }
        catch (Exception ex)
        {
            return Result.Failure<IReadOnlyList<Token>>(
                $"Failed to fetch tokens: {ex.Message}");
        }
    }
}
