using Application.Abstractions.Messaging;

namespace Application.SmsConfigs.Create;

public sealed class CreateSmsOtpCommand : ICommand<Guid>
{
    public string SmsToken { get; set; }
}
