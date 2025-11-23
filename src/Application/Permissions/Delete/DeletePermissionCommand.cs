using Application.Abstractions.Messaging;

namespace Application.Permissions.Delete;
public sealed record DeletePermissionCommand(Guid Id) : ICommand;
