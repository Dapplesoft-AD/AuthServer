using Application.Abstractions.Messaging;

namespace Application.PasswordResets.Delete;

public sealed record DeletePasswordResetCommand(Guid PR_Id) : ICommand;
