namespace Application.Token.GetById;

public sealed class TokenResponse
{
    public Guid TokenId { get; set; }
    public Guid UserId { get; set; }
    public Guid AppId { get; set; }
    public string Accesstoken { get; set; } //text
    public string Refreshtoken { get; set; } // text
    public DateTime IssuedAt { get; set; }
}
