using Application.Abstractions.Messaging;

namespace Application.Users.Verification.VerifyOtp;

public sealed record VerifyOtpCommand (
    string Destination,
    string OtpToken
) : ICommand;
