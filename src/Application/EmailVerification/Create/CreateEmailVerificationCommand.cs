using Application.Abstractions.Messaging;
using Domain.EmailVerification;

namespace Application.EmailVerification.Create;

public sealed class CreateEmailVerificationCommand : ICommand<Guid>
{
    public Guid User_Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires_at { get; set; }
    public DateTime Verified_at { get; set; }
}
