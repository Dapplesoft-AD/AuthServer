using SharedKernel;

namespace Domain.Otps;

public sealed class Otp : Entity
{
    public Guid OtpId { get; set; }
    public string OtpToken { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan Delay { get; set; } = TimeSpan.FromMinutes(3);
    public bool IsExpired { get; set; }
}
