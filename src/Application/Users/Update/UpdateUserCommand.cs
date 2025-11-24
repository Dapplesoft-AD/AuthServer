using System;
using Application.Abstractions.Messaging;
using Domain.Users;

namespace Application.Users.Update;

public sealed record UpdateUserCommand(
    Guid UserId,
    string? Fullname,
    string? Email,
    string? Password,
    string? Phone,
    UserStatus? Status,
    bool? IsMFAEnabled,
    bool? IsEmailVerified
    ) : ICommand;

