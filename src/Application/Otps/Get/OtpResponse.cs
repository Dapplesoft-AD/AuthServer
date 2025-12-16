using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Otps.Get;

public sealed class OtpResponse
{
    public Guid OtpId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsExpired { get; set; }
    public string OtpToken { get; set; }
    public TimeSpan Delay { get; set; }
    public DateTime CreatedAt { get; set; }
}
