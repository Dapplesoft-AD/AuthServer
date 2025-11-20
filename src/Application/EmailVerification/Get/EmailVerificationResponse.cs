namespace Application.EmailVerification.Get;

public sealed class EmailVerificationResponse
{
    public Guid EV_Id { get; set; }
    public Guid User_Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires_at { get; set; }
    public DateTime Verified_at { get; set; }
}
