using Application.Abstractions.Messaging;

namespace Application.Tokens.Get;

public sealed record GetTokensQuery() : IQuery<IReadOnlyList<SharedKernel.Models.Token>>;
