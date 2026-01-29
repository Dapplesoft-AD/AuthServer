using Application.Abstractions.Messaging;

namespace Application.Users.Verification.RequstOtpVerify;

public sealed record RequestOtpVerifyCommand(
    string Destination,
    double? Delay = 2
): ICommand<RequestOtpVerifyResponse>;
