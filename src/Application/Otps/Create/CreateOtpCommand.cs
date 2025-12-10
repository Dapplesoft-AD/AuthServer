using Application.Abstractions.Messaging;

namespace Application.Otps.Create;

public sealed record CreateOtpCommand(string? Email, string? PhoneNumber) : ICommand<Guid>;
