using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Roles.Delete;

internal sealed class DeleteRoleCommandHandler
    : ICommandHandler<DeleteRoleCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public DeleteRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        DeleteRoleCommand command,
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

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(command.Id);
    }
}
