using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.RolePermissions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.RolePermissions.Delete;

public sealed class DeleteRolePermissionCommandHandler
    : ICommandHandler<DeleteRolePermissionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRolePermissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteRolePermissionCommand command, CancellationToken cancellationToken)
    {
        RolePermission? rolePermission = await _context.RolePermissions
            .FindAsync(new object[] { command.RoleId, command.PermissionId }, cancellationToken);
        if(rolePermission is null)
        {
            return Result.Failure("Role permission not found.");
        }
        _context.RolePermissions.Remove(rolePermission);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
