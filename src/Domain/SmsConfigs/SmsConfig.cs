using SharedKernel;

namespace Domain.SmsConfigs;

public sealed class SmsConfig : Entity
{
    public Guid SmsId { get; set; }
    public string SmsToken { get; set; }
}
