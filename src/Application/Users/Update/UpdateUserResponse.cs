using Domain.Users;

namespace Application.Users.Update;

public sealed record UpdateUserResponse(
    string? Fullname,
    string? Email,
    string? Phone,
    UserStatus? Status,
    string? Address,
    bool? IsMFAEnabled,
    bool? IsEmailVerified
);
