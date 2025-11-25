using SharedKernel;

namespace Domain.Token;

public class Tokens : Entity
{
    public Guid TokenId { get; set; }
    public Guid UserId { get; set; }
    public Guid AppId { get; set; }
    public string Accesstoken { get; set; } = string.Empty; //text
    public string Refreshtoken { get; set; } = string.Empty; // text
    public DateTime IssuedAt { get; set; }

}
