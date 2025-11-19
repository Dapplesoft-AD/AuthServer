using System;
using Domain.MfaLogs;

namespace Application.MfaLogs.GetById;

public sealed class MfaLogResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime LoginTime { get; set; }
    public string IpAddress { get; set; } = null!;
    public string Device { get; set; } = null!;
    public MfaLogStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
