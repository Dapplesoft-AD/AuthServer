using Application.Abstractions.Messaging;

namespace Application.SmsConfigs.Create;

public sealed class CreateSmsOtpCommand : ICommand<Guid>
{
    public string? ProviderName { get; set; }
    public string? ProviderUrl { get; set; }
    public string ApiUrl { get; set; } = string.Empty;
    public string SmsToken { get; set; } = string.Empty;
}
