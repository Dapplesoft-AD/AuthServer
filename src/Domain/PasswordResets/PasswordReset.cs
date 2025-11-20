using SharedKernel;

namespace Domain.PasswordResets;

public class PasswordReset : Entity
{
    public Guid PrId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool Used { get; set; }
}
