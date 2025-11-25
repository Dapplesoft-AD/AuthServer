using Application.Abstractions.Messaging;

namespace Application.BusinessMembers.Update;

public sealed record UpdateBusinessMemberCommand(
    Guid Id,
    Guid BusinessId,
    Guid UserId,
    Guid RoleId
) : ICommand<Guid>;
