using FluentValidation;

namespace Application.Otps.GetById;

public class OtpResponse
{
    public Guid Id { get; set; }
    public string OtpToken { get; set; }
    public string Destination { get; set; }
    public DateTime ExpiresAt { get; set; }
    public TimeSpan Delay { get; set; } = TimeSpan.FromMinutes(2);
    public bool IsExpired { get; set; }
    public bool IsUsed { get; set; }
}
