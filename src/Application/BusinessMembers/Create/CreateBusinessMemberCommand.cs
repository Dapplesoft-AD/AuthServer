using Application.Abstractions.Messaging;

namespace Application.BusinessMembers.Create;

public sealed record CreateBusinessMemberCommand(
    Guid BusinessId,
    Guid UserId,
    Guid RoleId
) : ICommand<Guid>;
