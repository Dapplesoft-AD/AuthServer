namespace Application.Token.GetById;

public sealed class TokenResponse
{
    public Guid Id { get; set; }
    public Guid User_id { get; set; }
    public Guid App_id { get; set; }
    public string Acess_token { get; set; } //text
    public string Refresh_token { get; set; } // text
    public DateTime Issued_at { get; set; }
}
