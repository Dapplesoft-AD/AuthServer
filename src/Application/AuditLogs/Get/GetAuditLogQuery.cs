
using Application.Abstractions.Messaging;

namespace Application.AuditLogs.Get;
public sealed record GetAuditLogQuery()
    : IQuery<List<AuditLogResponse>>;
