using Application.Abstractions.Messaging;

namespace Application.Token.Create;

public sealed class CreateTokenCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public Guid AppId { get; set; }
    public string Accesstoken { get; set; } //text
    public string Refreshtoken { get; set; } // text
    public DateTime IssuedAt { get; set; }

}
