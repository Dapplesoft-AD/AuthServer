using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.UserLoginHistories;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using LoginStatus = Domain.UserLoginHistories.LoginStatus;

namespace Application.UserLoginHistories.Create;

internal sealed class CreateUserLoginHistoryCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext) : ICommandHandler<CreateUserLoginHistoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserLoginHistoryCommand command, CancellationToken cancellationToken)
    {
        if (userContext.UserId != command.UserId)
        {
            return Result.Failure<Guid>(UserErrors.Unauthorized());
        }

        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
        }

        var history = new UserLoginHistory
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            IpAddress = command.IpAddress,
            Country = command.Country,
            City = command.City,
            Browser = command.Browser,
            OS = command.OS,
            Device = command.Device,
            LogInTime = dateTimeProvider.UtcNow,
            LogoutTime = command.LogoutTime,
            Status = LoginStatus.Succeed
        };

        await context.UserLoginHistory.AddAsync(history, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return history.Id;
    }
}
