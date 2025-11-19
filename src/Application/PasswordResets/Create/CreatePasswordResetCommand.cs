using Application.Abstractions.Messaging;
using Domain.PasswordResets;

namespace Application.PasswordResets.Create;

public sealed class CreatePasswordResetCommand : ICommand<Guid>
{
    public Guid User_Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires_at { get; set; }
    public bool Used { get; set; }
}
