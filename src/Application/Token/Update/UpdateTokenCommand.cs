using Application.Abstractions.Messaging;

namespace Application.Token.Update;

public sealed record UpdateTokenCommand(
    Guid Id,
    Guid App_id ) : ICommand;
