namespace Application.EmailVerification.GetById;

public sealed class EmailVerificationResponse
{
    public Guid EvId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime VerifiedAt { get; set; }
}
