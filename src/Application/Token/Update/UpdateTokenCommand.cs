using Application.Abstractions.Messaging;

namespace Application.Token.Update;

public sealed record UpdateTokenCommand(
    Guid TokenId,
    Guid AppId) : ICommand;
