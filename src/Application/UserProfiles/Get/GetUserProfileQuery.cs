using Application.Abstractions.Messaging;
using Application.UserProfiles.Get;

namespace Application.UserProfiles.Create;
public sealed record GetUserProfileQuery(Guid UserId) : IQuery<UserProfileResponse>;
