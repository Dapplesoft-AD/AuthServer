namespace Application.AuditLogs.GetById;

public sealed class AuditLogResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BusinessId { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
