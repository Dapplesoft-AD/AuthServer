using Application.Abstractions.Messaging;
using Application.Token.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Token;

public sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("Tokens/{id:guid}", async (
            Guid id,
            IQueryHandler<GetTokenByIdQuery, TokenResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetTokenByIdQuery(id);

            Result<TokenResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Token)
        .RequireAuthorization();
    }
}
