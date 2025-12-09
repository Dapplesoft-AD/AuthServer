using Application.Abstractions.Messaging;

namespace Application.Otps.Verify;

public sealed record VerifyOtpCommand(string Email, string OtpToken) : ICommand<bool>;
