using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaLogs;
using SharedKernel;

namespace Application.MfaLogs.Create;

public class CreateMfaLogCommandHandler
    : ICommandHandler<CreateMfaLogCommand, Guid>
{
    private readonly IApplicationDbContext context;

    public CreateMfaLogCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<Guid>> Handle(
        CreateMfaLogCommand command,
        CancellationToken cancellationToken)
    {
        var mfaLog = new MfaLog
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            LoginTime = command.LoginTime,
            IpAddress = command.IpAddress,
            Device = command.Device,
            Status = command.Status
        };

        await context.MfaLogs.AddAsync(mfaLog, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(mfaLog.Id);
    }
}
