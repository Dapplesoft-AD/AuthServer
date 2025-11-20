using Application.Abstractions.Messaging;

namespace Application.EmailVerification.Delete;

public sealed record DeleteEmailVerificationCommand(Guid EV_Id) : ICommand;
