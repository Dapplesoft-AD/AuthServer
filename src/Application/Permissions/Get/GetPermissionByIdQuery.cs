using Application.Abstractions.Messaging;
using Application.Permissions.Get;

namespace Application.Permissions.GetById;
public sealed record GetPermissionByIdQuery(Guid Id) : IQuery<PermissionResponse>;
