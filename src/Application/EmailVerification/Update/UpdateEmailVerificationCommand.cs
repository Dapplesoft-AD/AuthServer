using Application.Abstractions.Messaging;

namespace Application.EmailVerification.Update;

public sealed record UpdateEmailVerificationCommand(
    Guid EV_Id,
    string Token) : ICommand;
