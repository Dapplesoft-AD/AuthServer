using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Todos;
using Domain.UserLoginHistories;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.UserLoginHistories.Delete;
internal class DeleteUserloginHistoryCommandHandler(IApplicationDbContext context, IUserContext userContext)
    : ICommandHandler<DeleteUserloginHistoryCommand>
{

    public async Task<Result> Handle(DeleteUserloginHistoryCommand command, CancellationToken cancellationToken)
    {
        UserLoginHistory? UserLoginHistory = await context.UserLoginHistory
            .SingleOrDefaultAsync(t => t.Id == command.Id && t.UserId == userContext.UserId, cancellationToken);

        if (UserLoginHistory is null)
        {
            return Result.Failure(UserloginHistoryErrors.NotFound(command.Id));
        }

        context.UserLoginHistory.Remove(UserLoginHistory);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
