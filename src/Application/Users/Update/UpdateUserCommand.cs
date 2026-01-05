using System;
using Application.Abstractions.Messaging;
using Domain.Users;

namespace Application.Users.Update;

public sealed record UpdateUserCommand(
    Guid UserId,
    string? Fullname,
    string? Email,
    string? Phone,
    Guid? CountryId,
    Guid? RegionId,
    Guid? DistrictId,
    Guid? SubDistrictId,
    UserStatus? Status,
    string? Address,
    bool? IsMFAEnabled,
    bool? IsEmailVerified
) : ICommand<UpdateUserResponse>;
