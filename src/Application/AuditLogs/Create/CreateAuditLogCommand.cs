using System;
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
