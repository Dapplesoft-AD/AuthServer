using Application.Abstractions.Messaging;

namespace Application.Roles.Update;

public sealed record UpdateRoleCommand(
    Guid Id,
    string RoleName,
    string Description
) : ICommand<Guid>;
