using System;
using Application.Abstractions.Messaging;


namespace Application.AuditLogs.Delete;
public sealed record DeleteAuditLogCommand(Guid Id) : ICommand;
