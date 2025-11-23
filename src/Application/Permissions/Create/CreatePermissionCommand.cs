using Application.Abstractions.Messaging;

namespace Application.Permissions.Create;
public sealed record CreatePermissionCommand(
    string Code,
    string Description
) : ICommand<Guid>;
