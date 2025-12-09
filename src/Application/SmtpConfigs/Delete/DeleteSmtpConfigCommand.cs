using Application.Abstractions.Messaging;

namespace Application.SmtpConfigs.Delete;

public sealed record DeleteSmtpConfigCommand(Guid SmtpId) : ICommand;
