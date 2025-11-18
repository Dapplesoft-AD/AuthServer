using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.UserRoles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.UserRoles.Assign;

internal sealed class AssignRoleToUserCommandHandler : ICommandHandler<AssignRoleToUserCommand>
{
    private readonly IApplicationDbContext _context;

    public AssignRoleToUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(AssignRoleToUserCommand command, CancellationToken cancellationToken)
    {
        bool userExists = await _context.Users
            .AnyAsync(u => u.Id == command.UserId, cancellationToken);

        if (!userExists)
        {
            return Result.Failure(UserErrors.NotFound(command.UserId));
        }

        bool roleExists = await _context.Roles
            .AnyAsync(r => r.Id == command.RoleId, cancellationToken);

        if (!roleExists)
        {
            return Result.Failure(RoleErrors.NotFound(command.RoleId));
        }

        bool applicationExists = await _context.Applications
            .AnyAsync(a => a.Id == command.ApplicationId, cancellationToken);

        if (!applicationExists)
        {
            return Result.Failure(ApplicationErrors.NotFound(command.ApplicationId));
        }

        bool alreadyAssigned = await _context.UserRoles
            .AnyAsync(ur =>
                ur.UserId == command.UserId &&
                ur.RoleId == command.RoleId &&
                ur.ApplicationId == command.ApplicationId,
                cancellationToken);

        if (alreadyAssigned)
        {
            return Result.Failure(UserRoleErrors.AlreadyAssigned(command.UserId, command.RoleId));
        }

        var userRole = new UserRole
        {
            UserId = command.UserId,
            RoleId = command.RoleId,
            ApplicationId = command.ApplicationId,
            AssignedAt = DateTime.UtcNow
        };

        await _context.UserRoles.AddAsync(userRole, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

// Error classes defined in the same file
internal static class UserRoleErrors
{
    public static Error AlreadyAssigned(Guid userId, Guid roleId) => Error.Conflict(
        "UserRoles.AlreadyAssigned",
        $"The user with ID '{userId}' already has the role with ID '{roleId}' assigned");
}

internal static class ApplicationErrors
{
    public static Error NotFound(Guid applicationId) => Error.NotFound(
        "Applications.NotFound",
        $"The application with Id = '{applicationId}' was not found");
}

internal static class RoleErrors
{
    public static Error NotFound(Guid roleId) => Error.NotFound(
        "Roles.NotFound",
        $"The role with Id = '{roleId}' was not found");
}

internal static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with Id = '{userId}' was not found");
}
