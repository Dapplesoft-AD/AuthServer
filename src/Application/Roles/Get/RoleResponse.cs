namespace Application.Roles.Get;

public sealed record RoleResponse(
    Guid Id,
    string RoleName,
    string Description
);
