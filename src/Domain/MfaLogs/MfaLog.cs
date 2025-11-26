using System;

namespace Domain.MfaLogs;

public class MfaLog
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime LoginTime { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string IpAddress { get; set; } = null!;

    public string Device { get; set; } = null!;

    // Remove JsonConverter - let EF Core handle the conversion
    public MfaLogStatus Status { get; set; }
}

public enum MfaLogStatus
{
    Success = 1,
    Failed = 2
}
