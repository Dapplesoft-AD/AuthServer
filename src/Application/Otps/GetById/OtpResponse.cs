using FluentValidation;

namespace Application.Otps.GetById;

public class OtpResponse
{
    public Guid OtpId { get; set; }
    public string OtpToken { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan Delay { get; set; }
    public bool IsExpired { get; set; }
}
