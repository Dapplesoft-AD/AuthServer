using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaSettings.Delete;

internal sealed class DeleteMfaSettingCommandHandler
    : ICommandHandler<DeleteMfaSettingCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMfaSettingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteMfaSettingCommand command, CancellationToken cancellationToken)
    {
        MfaSetting? mfa = await _context.MfaSettings
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (mfa is null)
        {
            return Result.Failure(
                Error.NotFound("MfaSetting.NotFound", $"MfaSetting with Id {command.Id} not found.")
            );
        }

        _context.MfaSettings.Remove(mfa);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
