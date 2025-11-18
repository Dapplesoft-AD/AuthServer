<<<<<<< HEAD
﻿using Application.Abstractions.Authorization;
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
=======
﻿namespace Infrastructure.Authorization;

internal sealed class PermissionProvider
{
    public Task<HashSet<string>> GetForUserIdAsync(Guid userId)
    {
        // TODO: Here you'll implement your logic to fetch permissions.
        HashSet<string> permissionsSet = [];

        return Task.FromResult(permissionsSet);
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79
    }
}
