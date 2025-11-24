using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.RolePermissions.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;


namespace Application.RolePermissions.GetAll;

public sealed class GetAllRolePermissionsQueryHandler
    : IQueryHandler<GetAllRolePermissionsQuery, List<RolePermissionResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllRolePermissionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<RolePermissionResponse>>> Handle(GetAllRolePermissionsQuery query, CancellationToken cancellationToken)
    {
        List<RolePermissionResponse> rolePermissions = await _context.RolePermissions
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
