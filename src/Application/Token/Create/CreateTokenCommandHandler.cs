using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Token;
using SharedKernel;

namespace Application.Token.Create;

public class CreateTokenCommandHandler : ICommandHandler<CreateTokenCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    public CreateTokenCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }
    public async Task<Result<Guid>> Handle(CreateTokenCommand command, CancellationToken cancellationToken)
    {
        var token = new Tokens
        {
            UserId = command.UserId,
            AppId = command.AppId,
            Accesstoken = command.Accesstoken,
            Refreshtoken = command.Refreshtoken,
            IssuedAt = command.IssuedAt == default
                ? _dateTimeProvider.UtcNow
                : command.IssuedAt
        };

        token.Raise(new TokenCreatedDomainEvent(token.TokenId));
        await _context.Tokens.AddAsync(token, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(token.TokenId);
    }
}
