using Application.Abstractions.Messaging;
using Application.Token.Get;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("Tokens", async (
            Guid userId,
            IQueryHandler<GetTokensQuery, List<TokenResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetTokensQuery(userId);

            Result<List<TokenResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Token)
        .RequireAuthorization();
    }
}
