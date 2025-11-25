using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Roles.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Roles.GetById;

internal sealed class GetRoleByIdQueryHandler
    : IQueryHandler<GetRoleByIdQuery, RoleResponse>
{
    private readonly IApplicationDbContext _context;
    public GetRoleByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<RoleResponse>> Handle(
        GetRoleByIdQuery query,
        CancellationToken cancellationToken)
    {
        RoleResponse? role = await _context.Roles
            .Where(r => r.Id == query.Id)
            .Select(r => new RoleResponse(
                r.Id,
                r.RoleName,
                r.Description
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null)
        {
            return Result.Failure<RoleResponse>(Error.NotFound(
                "Role.NotFound",
                "Role not found."
            ));
        }

        return Result.Success(role);
    }
}
