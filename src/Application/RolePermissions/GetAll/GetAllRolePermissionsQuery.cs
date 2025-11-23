using Application.Abstractions.Messaging;
using Application.RolePermissions.Get;

namespace Application.RolePermissions.GetAll;
public sealed record GetAllRolePermissionsQuery() : IQuery<List<RolePermissionResponse>>;
