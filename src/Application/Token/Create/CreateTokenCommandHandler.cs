using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Application.Create;
using Domain.Application;
using Domain.PasswordResets;
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
            User_id = command.User_id,
            App_id = command.App_id,
            Access_token = command.Access_token,
            Refresh_token = command.Refresh_token,
            Issued_at = command.Issued_at == default
                ? _dateTimeProvider.UtcNow
                : command.Issued_at
        };

        token.Raise(new TokenCreatedDomainEvent(token.TokenId));
        await _context.Tokens.AddAsync(token, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(token.TokenId);
    }
}
