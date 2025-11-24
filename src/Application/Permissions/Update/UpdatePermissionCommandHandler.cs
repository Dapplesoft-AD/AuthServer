using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Permissions.Update;
public sealed class UpdatePermissionCommandHandler
    : ICommandHandler<UpdatePermissionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdatePermissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        Domain.Permissions.Permission? permission = await _context.Permissions
            .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
        if (permission is null)
        {
            return Result.Failure<Guid>("permission not found.");
        }
        // Check if code already exists (excluding current permission)
        bool codeExists = await _context.Permissions
            .AnyAsync(p => p.Code == command.Code && p.Id != command.Id, cancellationToken);

        if (codeExists)
        {
            return Result.Failure<Guid>("Permission code already exists.");
        }

        permission.Code = command.Code;
        permission.Description = command.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(permission.Id);
    }
}
