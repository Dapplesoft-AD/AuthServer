using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.RolePermissions.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.RolePermissions.Get;

public sealed class GetRolePermissionQueryHandler
    : IQueryHandler<GetRolePermissionQuery, RolePermissionResponse>
{
    private readonly IApplicationDbContext _context;

    public GetRolePermissionQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<RolePermissionResponse>> Handle(GetRolePermissionQuery query, CancellationToken cancellationToken)
    {
        RolePermissionResponse rolePermission = await _context.RolePermissions
            .Where(rp => rp.RoleId == query.RoleId && rp.PermissionId == query.PermissionId)
            .Select(rp => new RolePermissionResponse(
                rp.RoleId,
                rp.Role.RoleName,
                rp.PermissionId,
                rp.Permission.Code,
                rp.Permission.Description
            ))
            .FirstOrDefaultAsync(cancellationToken) ;
        if (rolePermission is null)
        {
            return Result.Failure<RolePermissionResponse>("Role permission not found.");
        }

        return Result.Success(rolePermission);
    }
}
