using SharedKernel;

namespace Domain.SmsConfigs;

public sealed class SmsConfig : Entity
{
    public Guid Id { get; set; }
    public string? ProviderName { get; set; } = null!;
    public string? ProviderUrl { get; set; } = null!;
    public string ApiUrl { get; set; } = null!;
    public string Token { get; set; }
    public bool IsActive { get; set; } = true;
}
