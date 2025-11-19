using Application.Abstractions.Messaging;

namespace Application.EmailVerification.Update;

public sealed record UpdateEmailVerificationCommand(
    Guid Id,
    string Token) : ICommand;
