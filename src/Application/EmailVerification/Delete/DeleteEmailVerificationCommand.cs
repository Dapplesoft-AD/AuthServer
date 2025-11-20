using Application.Abstractions.Messaging;

namespace Application.EmailVerification.Delete;

public sealed record DeleteEmailVerificationCommand(Guid EvId) : ICommand;
