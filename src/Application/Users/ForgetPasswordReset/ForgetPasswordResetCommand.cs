using Application.Abstractions.Messaging;

namespace Application.Users.ForgetPasswordReset;
public sealed record ForgetPasswordResetCommand(
    string Email,
    string NewPassword,
    string ConfirmPassword) : ICommand<ForgetPasswordResetResponse>;
