using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Todos;
using Domain.Token;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Token.Update;

internal sealed class UpdateTokenCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<UpdateTokenCommand>
{
    public async Task<Result> Handle(UpdateTokenCommand command, CancellationToken cancellationToken)
    {
        Tokens? token = await context.Tokens
            .SingleOrDefaultAsync(t => t.Id == command.Id, cancellationToken);

        if (token is null)
        {
            return Result.Failure(TodoItemErrors.NotFound(command.Id));
        }

        token.App_id = command.App_id;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
