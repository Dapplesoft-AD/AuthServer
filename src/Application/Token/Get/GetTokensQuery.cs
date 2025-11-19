using Application.Abstractions.Messaging;

namespace Application.Token.Get;

public sealed record GetTokensQuery(Guid User_id) : IQuery<List<TokenResponse>>;
