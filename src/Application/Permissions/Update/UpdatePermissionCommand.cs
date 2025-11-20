using Application.Abstractions.Messaging;

namespace Application.Permissions.Update;
public sealed record UpdatePermissionCommand(
    Guid Id,
    string Code,
    string Description
) : ICommand<Guid>;
