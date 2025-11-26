using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.AuditLogs.Update;
public sealed record UpdateAuditLogCommand(
    Guid AuditLogId,
    string Action,
    string Description
) : ICommand;
