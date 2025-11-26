using System;
namespace Application.AuditLogs.Get;
public sealed class AuditLogResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BusinessId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
