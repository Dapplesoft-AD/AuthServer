using SharedKernel;

namespace Domain.Otps;

public sealed class Otp : Entity
{
    public Guid Id { get; set; }
    public string OtpToken { get; set; }
    public string Destination { get; set; }
    public OtpType OtpType { get; set; } = OtpType.Default;
    public TimeSpan Delay { get; set; } = TimeSpan.FromMinutes(2);
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsExpired { get; set; }
    public bool IsUsed { get; set; }
}
