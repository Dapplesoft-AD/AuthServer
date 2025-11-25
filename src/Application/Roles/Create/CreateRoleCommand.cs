using Application.Abstractions.Messaging;

namespace Application.Roles.Create;

public sealed record CreateRoleCommand(
    string RoleName,
    string Description
) : ICommand<Guid>;
