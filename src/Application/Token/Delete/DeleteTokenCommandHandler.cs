using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Token;
using Microsoft.EntityFrameworkCore;
using SharedKernel;


namespace Application.Token.Delete;

internal sealed class DeleteTokenCommandHandler : ICommandHandler<DeleteTokenCommand>
{

    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    public DeleteTokenCommandHandler(IApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }
    public async Task<Result> Handle(DeleteTokenCommand command, CancellationToken cancellationToken)
    {
        Tokens? tokens = await _context.Tokens
            .FirstOrDefaultAsync(t => t.Id == command.Id && t.User_id == _userContext.UserId, cancellationToken);
        if(tokens is null)
        {
            return Result.Failure(TokenErrors.NotFound(command.Id));
        }
        _context.Tokens.Remove(tokens);
        tokens.Raise(new TokenDeletedDomainEvent(tokens.Id));
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
