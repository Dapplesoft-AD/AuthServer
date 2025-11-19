using System;
using Domain.MfaLogs;

namespace Application.MfaLogs.Get;
public sealed class MfaLogResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime LoginTime { get; set; }
    public string IpAddress { get; set; }
    public string Device { get; set; }
    public MfaLogStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
