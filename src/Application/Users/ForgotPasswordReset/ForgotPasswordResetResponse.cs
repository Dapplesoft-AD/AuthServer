namespace Application.Users.ForgotPasswordReset;

public sealed record ForgotPasswordResetResponse(
    bool Success,
    string Message
);
