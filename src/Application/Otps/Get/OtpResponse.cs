using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Otps.Get;

public sealed class OtpResponse
{
    public Guid OtpId { get; set; }
    public string OtpToken { get; set; }
    public string Destination { get; set; }
    public DateTime ExpiresAt { get; set; }
    public TimeSpan Delay { get; set; } = TimeSpan.FromMinutes(2);
    public bool IsExpired { get; set; }
    public bool IsUsed { get; set; }
}
