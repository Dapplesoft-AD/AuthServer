
namespace Application.Users.Verification.RequstOtpVerify;

public sealed record RequestOtpVerifyResponse(
    Guid Id,
    string? Destination = null,
    DateTime? ExpiresAt = null,
    DateTime? CreatedAt = null,
    string? Message = null,
    double? Delay = 0,
    int? WaitForSeconds = 0
);
