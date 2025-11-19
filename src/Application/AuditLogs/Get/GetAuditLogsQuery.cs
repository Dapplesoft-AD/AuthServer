using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.AuditLogs.Get;
public sealed record GetAuditLogsQuery()
    : IQuery<List<AuditLogResponse>>;
