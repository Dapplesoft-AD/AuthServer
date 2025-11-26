using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaLogs.Delete;

internal sealed class DeleteMfaLogCommandHandler
    : ICommandHandler<DeleteMfaLogCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMfaLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteMfaLogCommand command, CancellationToken cancellationToken)
    {
        MfaLog? mfaLog = await _context.MfaLogs
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (mfaLog is null)
        {
            return Result.Failure(MfaLogErrors.NotFound(command.Id));
        }

        _context.MfaLogs.Remove(mfaLog);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
