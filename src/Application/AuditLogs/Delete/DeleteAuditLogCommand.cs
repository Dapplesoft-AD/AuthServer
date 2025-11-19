using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;


namespace Application.AuditLogs.Delete;
public sealed record DeleteAuditLogCommand(Guid Id) : ICommand;
