using Application.Abstractions.Messaging;
using Application.Token.GetById;

namespace Application.Token.GetById;

public sealed record GetTokenByIdQuery(Guid TokenId) : IQuery<TokenResponse>;
