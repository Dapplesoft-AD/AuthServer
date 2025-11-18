using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Delete;
internal class DeleteUserCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<DeleteUserCommand>
{

    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        // user can only delete their own account

        User? user = await context.Users
            .SingleOrDefaultAsync(u => u.Id == command.Id && u.Id == userContext.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(command.Id));
        }

        context.Users.Remove(user);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
