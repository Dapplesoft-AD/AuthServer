namespace Application.Permissions.Get;
public sealed record PermissionResponse(
    Guid Id,
    string Code,
    string Description
);
