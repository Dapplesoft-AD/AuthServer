using Application.Abstractions.Messaging;
using Application.Roles.Get;

namespace Application.Roles.GetById;

public sealed record GetRoleByIdQuery(Guid Id) : IQuery<RoleResponse>;
