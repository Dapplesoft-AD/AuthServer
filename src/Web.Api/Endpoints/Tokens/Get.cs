using System.Collections.Generic;
using Application.Abstractions.Messaging;
using Application.Tokens.Get;
using SharedKernel;
using SharedKernel.Models;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Tokens;

public sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tokens", async (
            IQueryHandler<GetTokensQuery, IReadOnlyList<Token>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetTokensQuery();

            Result<IReadOnlyList<Token>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Token)
        .RequireAuthorization();
    }
}
