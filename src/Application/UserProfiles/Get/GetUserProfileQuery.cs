using Application.Abstractions.Messaging;
using Application.UserProfiles.Get;

namespace Application.UserProfiles.Get;

public sealed record GetUserProfileQuery(Guid UserId) : IQuery<UserProfileResponse>;
