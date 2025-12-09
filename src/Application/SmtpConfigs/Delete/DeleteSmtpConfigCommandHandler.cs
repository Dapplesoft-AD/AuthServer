using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SmtpConfigs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;


namespace Application.SmtpConfigs.Delete;

internal sealed class DeleteSmtpConfigCommandHandler : ICommandHandler<DeleteSmtpConfigCommand>
{

    private readonly IApplicationDbContext _context;
    public DeleteSmtpConfigCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteSmtpConfigCommand command, CancellationToken cancellationToken)
    {
        SmtpConfig? smtpConfig = await _context.SmtpConfig
            .FirstOrDefaultAsync(t => t.SmtpId == command.SmtpId, cancellationToken);
        if (smtpConfig is null)
        {
            return Result.Failure(SmtpConfigErrors.NotFound(command.SmtpId));
        }
        _context.SmtpConfig.Remove(smtpConfig);
        smtpConfig.Raise(new SmtpConfigDeletedDomainEvent(smtpConfig.SmtpId));
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
