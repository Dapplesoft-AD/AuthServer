using SharedKernel;

namespace Domain.EmailVerification;

public class EmailVerifications : Entity
{
    public Guid EvId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime VerifiedAt { get; set; }
}
