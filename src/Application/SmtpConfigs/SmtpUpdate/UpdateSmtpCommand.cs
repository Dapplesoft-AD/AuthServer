using Application.Abstractions.Messaging;

namespace Application.SmtpConfigs.SmtpUpdate;

public sealed record UpdateSmtpCommand(
    Guid SmtpId,
    string Username,
    string Password,
    string SenderEmail) : ICommand<Guid>;
