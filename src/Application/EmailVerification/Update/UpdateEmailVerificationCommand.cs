using Application.Abstractions.Messaging;

namespace Application.EmailVerification.Update;

public sealed record UpdateEmailVerificationCommand(
    Guid EvId,
    string Token) : ICommand;
