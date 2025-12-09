using Application.Abstractions.Messaging;
namespace Application.SmtpConfigs.OtpMail;

public sealed record SendOtpCommand(string RecipientEmail) : ICommand<Guid>;
