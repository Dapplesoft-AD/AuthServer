using Application.Abstractions.Messaging;
using Application.RolePermissions.Get;

namespace Application.RolePermissions.GetByRoleId;
public sealed record GetRolePermissionsByRoleIdQuery(Guid RoleId) : IQuery<List<RolePermissionResponse>>;
