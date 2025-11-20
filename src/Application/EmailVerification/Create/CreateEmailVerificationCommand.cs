using Application.Abstractions.Messaging;

namespace Application.EmailVerification.Create;

public sealed class CreateEmailVerificationCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime VerifiedAt { get; set; }
}
