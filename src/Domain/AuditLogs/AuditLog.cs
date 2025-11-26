using System;
using Domain.Users;
using SharedKernel;

namespace Domain.AuditLogs;
public class AuditLog : Entity
{
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid UserId { get; set; }
    public Guid BusinessId { get; set; }

    // Navigation Properties (optional but recommended in DDD)
    public User User { get; set; }
  
    // Audit Information
    public string Action { get; set; }         
    public string Description { get; set; }     

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
