using Application.Abstractions.Messaging;

namespace Application.PasswordResets.Update;

public sealed record UpdatePasswordResetCommand(
    Guid Id,
    string Token) : ICommand;
