using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Update;

internal sealed class UpdateUserCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IPasswordHasher passwordHasher
    ) : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {

        User? userTuple = await context.Users
           .SingleOrDefaultAsync(t => t.Id == command.UserId, cancellationToken);

        if (userTuple is null)
        {
            return Result.Failure(TodoItemErrors.NotFound(command.UserId));
        }

        if (command.Fullname is not null)
        {
            userTuple.FullName = command.Fullname;
        }

        if (command.Email is not null)
        {
            userTuple.Email = command.Email;
        }

        if (command.Password is not null)
        {
            userTuple.PasswordHash = passwordHasher.Hash(command.Password);
        }

        if (command.Phone is not null)
        {
            userTuple.Phone = command.Phone;
        }

        if (command.Status.HasValue)
        {
            userTuple.Status = command.Status.Value;
        }

        if (command.IsMFAEnabled.HasValue)
        {
            userTuple.IsMFAEnabled = command.IsMFAEnabled.Value;
        }

        if (command.IsEmailVarified.HasValue)
        {
            userTuple.IsEmailVarified = command.IsEmailVarified.Value;
        }

        if (context.Entry(userTuple).State == EntityState.Modified)
        {
            userTuple.UpdatedAt = dateTimeProvider.UtcNow;
            await context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
