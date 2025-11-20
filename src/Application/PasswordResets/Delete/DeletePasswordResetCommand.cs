using Application.Abstractions.Messaging;

namespace Application.PasswordResets.Delete;

public sealed record DeletePasswordResetCommand(Guid PrId) : ICommand;
