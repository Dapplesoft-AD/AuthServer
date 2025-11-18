using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Roles.Create;

public sealed class CreateRoleCommandHandler
    : ICommandHandler<CreateRoleCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        // Check if role name already exists
        bool roleExists = await _context.Roles
            .AnyAsync(r => r.RoleName == command.RoleName, cancellationToken);

        if (roleExists)
        {
            throw new InvalidOperationException("Role name already exists.");
        }

        var role = new Role
        {
            Id = Guid.NewGuid(),
            RoleName = command.RoleName,
            Description = command.Description
        };

        await _context.Roles.AddAsync(role, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(role.Id);
    }
}
