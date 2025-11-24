using Application.Abstractions.Messaging;
using Application.Permissions.Get;

namespace Application.Permissions.GetAll;
public sealed record GetAllPermissionsQuery() : IQuery<List<PermissionResponse>>;
