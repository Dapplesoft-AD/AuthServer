using Application.Abstractions.Messaging;

namespace Application.PasswordResets.Update;

public sealed record UpdatePasswordResetCommand(
    Guid PR_Id,
    string Token) : ICommand;
