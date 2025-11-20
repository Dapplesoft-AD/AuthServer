using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaSettings;
using SharedKernel;

namespace Application.MfaSettings.Create;

public class CreateMfaSettingCommandHandler
    : ICommandHandler<CreateMfaSettingCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateMfaSettingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        CreateMfaSettingCommand command,
        CancellationToken cancellationToken)
    {
        var mfaSetting = new MfaSetting
        {
            UserId = command.UserId,
            SecretKey = command.SecretKey,
            BackupCodes = command.BackupCodes,
            Method = command.Method, // already enum
            Enabled = command.Enabled
        };

        await _context.MfaSettings.AddAsync(mfaSetting, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(mfaSetting.Id);
    }
}
