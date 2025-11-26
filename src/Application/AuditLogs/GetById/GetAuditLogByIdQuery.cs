using Application.Abstractions.Messaging;

namespace Application.AuditLogs.GetById;

public sealed record GetAuditLogByIdQuery(Guid Id) : IQuery<AuditLogResponse>;
