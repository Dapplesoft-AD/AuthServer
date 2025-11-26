using Application.Abstractions.Messaging;
using Domain.MfaLogs;

namespace Application.MfaLogs.Update;

public sealed record UpdateMfaLogCommand(
    Guid MfaLogId,
    DateTime LoginTime,
    string IpAddress,
    string Device,
    MfaLogStatus Status
) : ICommand;
