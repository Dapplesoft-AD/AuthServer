using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Roles.Create;

internal sealed class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        // Check if RoleName already exists
        bool exists = await _context.Roles.AnyAsync(r => r.RoleName == command.RoleName, cancellationToken);
        if (exists)
        {
            return Result.Failure<Guid>(Error.Conflict("Role.Exists", "Role name already exists."));
        }

        var role = new Role
        {
            Id = Guid.NewGuid(),
            RoleName = command.RoleName,
            Description = command.Description
        };

        _context.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(role.Id);
    }
}
