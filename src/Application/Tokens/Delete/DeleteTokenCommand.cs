using Application.Abstractions.Messaging;

namespace Application.Tokens.Delete;

public sealed record DeleteTokenCommand(string TokenId) : ICommand;
