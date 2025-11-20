using Application.Abstractions.Messaging;

namespace Application.Token.Delete;

public sealed record DeleteTokenCommand(Guid TokenId) : ICommand;
