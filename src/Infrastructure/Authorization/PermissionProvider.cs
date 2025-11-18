using Application.Abstractions.Authorization;
using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authorization;

internal sealed class PermissionProvider(IApplicationDbContext context) : IPermissionProvider
{
    public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        List<string> permissions = await context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(context.RolePermissions,
                  ur => ur.RoleId,
                  rp => rp.RoleId,
                  (ur, rp) => rp)
            .Join(context.Permissions,
                  rp => rp.PermissionId,
                  p => p.Id,
                  (rp, p) => p.Code)
            .Distinct()
            .ToListAsync();

        return permissions.ToHashSet();
    }
}
