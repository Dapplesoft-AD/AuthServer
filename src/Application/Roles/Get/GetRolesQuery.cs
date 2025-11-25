using Application.Abstractions.Messaging;

namespace Application.Roles.Get;

public sealed record GetRolesQuery() : IQuery<List<RoleResponse>>;
