using System;
using System.Collections.Generic;
using System.Text;
using Domain.Users;

namespace Application.Users.Update;

public sealed record UpdateUserResponse(
    string? Fullname,
    string? Email,
    string? Phone,
    UserStatus? Status,
    bool? IsMFAEnabled,
    bool? IsEmailVerified
);
