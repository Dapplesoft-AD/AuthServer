using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Permissions;
using SharedKernel;

namespace Application.Permissions.Create;

public sealed class CreatePermissionCommandHandler
    : ICommandHandler<CreatePermissionCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreatePermissionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        var permission = new Permission
        {
            Id = Guid.NewGuid(),
            Code = command.Code,
            Description = command.Description
        };

        await _context.Permissions.AddAsync(permission, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(permission.Id);
    }
}
