using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Roles.Update;

internal sealed class UpdateRoleCommandHandler
    : ICommandHandler<UpdateRoleCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public UpdateRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        UpdateRoleCommand command,
        CancellationToken cancellationToken)
    {
        Domain.Roles.Role? role = await _context.Roles
            .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);

        if (role is null)
        {
            return Result.Failure<Guid>(Error.NotFound(
                "Role.NotFound",
                "Role not found."
            ));
        }

        bool exists = await _context.Roles
            .AnyAsync(r => r.RoleName == command.RoleName && r.Id != command.Id,
            cancellationToken);

        if (exists)
        {
            return Result.Failure<Guid>(Error.Conflict(
                "Role.Exists",
                "Role name already exists."
            ));
        }

        role.RoleName = command.RoleName;
        role.Description = command.Description;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(role.Id);
    }
}
