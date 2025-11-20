using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.RolePermissions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.RolePermissions.Create;

public sealed class CreateRolePermissionCommandHandler
    : ICommandHandler<CreateRolePermissionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateRolePermissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateRolePermissionCommand command, CancellationToken cancellationToken)
    {
        // Check if role exists
        bool roleExists = await _context.Roles
            .AnyAsync(r => r.Id == command.RoleId, cancellationToken);

        if (!roleExists)
        {
            return Result.Failure<Guid>("The specified role does not exist.");
        }

        // Check if permission exists
        bool permissionExists = await _context.Permissions
            .AnyAsync(p => p.Id == command.PermissionId, cancellationToken);

        if (!permissionExists)
        {
            return Result.Failure<Guid>("The specified permission does not exist.");
        }

        // Check if the relationship already exists
        RolePermission? existingRolePermission = await _context.RolePermissions
            .FindAsync(new object[] { command.RoleId, command.PermissionId }, cancellationToken);

        if (existingRolePermission != null)
        {
            return Result.Failure<Guid>("This role already has the specified permission.");
        }

        var rolePermission = new RolePermission
        {
            RoleId = command.RoleId,
            PermissionId = command.PermissionId
        };

        await _context.RolePermissions.AddAsync(rolePermission, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(rolePermission.RoleId);
    }
}
