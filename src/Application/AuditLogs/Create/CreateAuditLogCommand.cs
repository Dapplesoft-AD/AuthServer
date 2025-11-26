using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.AuditLogs.Create;
public sealed class CreateAuditLogCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public Guid BusinessId { get; set; }

    public string Action { get; set; }         
    public string Description { get; set; }     // Details

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
