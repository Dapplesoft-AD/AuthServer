using Application.Abstractions.Messaging;

namespace Application.PasswordResets.Create;

public sealed class CreatePasswordResetCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool Used { get; set; }
}
