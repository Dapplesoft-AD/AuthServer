using Application.Abstractions.Messaging;

namespace Application.Token.GetById;

public sealed record GetTokenByIdQuery(Guid TokenId) : IQuery<TokenResponse>;
