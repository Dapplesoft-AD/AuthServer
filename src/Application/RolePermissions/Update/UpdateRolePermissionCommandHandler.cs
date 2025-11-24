using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.RolePermissions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.RolePermissions.Update;

public sealed class UpdateRolePermissionCommandHandler
    : ICommandHandler<UpdateRolePermissionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateRolePermissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(UpdateRolePermissionCommand command, CancellationToken cancellationToken)
    {
        // Check if the existing role permission exists
        RolePermission? existingRolePermission = await _context.RolePermissions
            .FindAsync(new object[] { command.RoleId, command.PermissionId }, cancellationToken);
        if (existingRolePermission is null)
        {
            return Result.Failure<Guid>("Role permission not found.");
        }
        // Check if new permission exists
        bool newPermissionExists = await _context.Permissions
            .AnyAsync(p => p.Id == command.NewPermissionId, cancellationToken);

        if (!newPermissionExists)
        {
            return Result.Failure<Guid>("The specified new permission does not exist.");
        }

        // Check if the new relationship already exists
        RolePermission? newRolePermissionExists = await _context.RolePermissions
            .FindAsync(new object[] { command.RoleId, command.NewPermissionId }, cancellationToken);

        if (newRolePermissionExists != null)
        {
            return Result.Failure<Guid>("Role permission not found.");
        }

        // Remove old relationship and create new one
        _context.RolePermissions.Remove(existingRolePermission);

        var newRolePermission = new RolePermission
        {
            RoleId = command.RoleId,
            PermissionId = command.NewPermissionId
        };

        await _context.RolePermissions.AddAsync(newRolePermission, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(newRolePermission.RoleId);
    }
}
