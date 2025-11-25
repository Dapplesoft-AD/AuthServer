namespace Application.PasswordResets.Get;

public sealed class PasswordResetResponse
{
    public Guid PrId { get; set; }
    public Guid User_Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires_at { get; set; }
    public bool Used { get; set; }
}
