using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
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
            .SingleOrDefaultAsync(t => t.TokenId == command.TokenId, cancellationToken);

        if (token is null)
        {
            return Result.Failure(TokenErrors.NotFound(command.TokenId));
        }

        token.AppId = command.AppId;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
