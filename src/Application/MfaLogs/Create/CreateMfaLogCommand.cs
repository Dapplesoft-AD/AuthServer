using Application.Abstractions.Messaging;
using Domain.MfaLogs;

namespace Application.MfaLogs.Create;

public sealed class CreateMfaLogCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public DateTime LoginTime { get; set; } = DateTime.UtcNow;
    public string IpAddress { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public MfaLogStatus Status { get; set; } // Use enum directly
}
