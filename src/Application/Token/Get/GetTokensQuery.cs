using Application.Abstractions.Messaging;

namespace Application.Token.Get;

public sealed record GetTokensQuery(Guid UserId) : IQuery<List<TokenResponse>>;
