using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.RolePermissions.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.RolePermissions.GetByRoleId;

public sealed class GetRolePermissionsByRoleIdQueryHandler
    : IQueryHandler<GetRolePermissionsByRoleIdQuery, List<RolePermissionResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetRolePermissionsByRoleIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<RolePermissionResponse>>> Handle(GetRolePermissionsByRoleIdQuery query, CancellationToken cancellationToken)
    {
        List<RolePermissionResponse> rolePermissions = await _context.RolePermissions
            .Where(rp => rp.RoleId == query.RoleId)
            .Select(rp => new RolePermissionResponse(
                rp.RoleId,
                rp.Role.RoleName,
                rp.PermissionId,
                rp.Permission.Code,
                rp.Permission.Description
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(rolePermissions);
    }
}
