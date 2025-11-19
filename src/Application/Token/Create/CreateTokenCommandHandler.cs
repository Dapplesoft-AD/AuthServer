using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Application;
using Application.Application.Create;
using SharedKernel;
using Domain.Token;

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
            Acess_token = command.Acess_token,
            Refresh_token = command.Refresh_token,
            Issued_at = command.Issued_at == default
                ? _dateTimeProvider.UtcNow
                : command.Issued_at
        };

        await _context.Tokens.AddAsync(token, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(token.Id);
    }
}
