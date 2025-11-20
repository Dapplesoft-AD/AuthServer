using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Permissions.Get;
using Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Permissions.Delete;

public sealed class DeletePermissionCommandHandler
    : ICommandHandler<DeletePermissionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePermissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeletePermissionCommand command, CancellationToken cancellationToken)
    {
        Permission? permission = await _context.Permissions
            .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
        if (permission is null)
        {
            return Result.Failure<PermissionResponse>("Permission not found.");
        }
        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
