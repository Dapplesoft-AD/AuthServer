using System;
using Application.Abstractions.Messaging;

namespace Application.AuditLogs.Update;
public sealed record UpdateAuditLogCommand(
    Guid AuditLogId,
    string Action,
    string Description
) : ICommand;
