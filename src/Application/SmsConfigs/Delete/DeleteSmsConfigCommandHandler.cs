using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SmsConfigs;
using SharedKernel;

namespace Application.SmsConfigs.Delete;

internal sealed class DeleteSmsConfigCommandHandler(
    IApplicationDbContext applicationDbContext) : ICommandHandler<DeleteSmsConfigCommand>
{
    public async Task<Result> Handle(DeleteSmsConfigCommand request, CancellationToken cancellationToken)
    {
        SmsConfig? smsConfig = await applicationDbContext.SmsConfig
            .FindAsync([request.SmsId], cancellationToken);
        if (smsConfig is null)
        {
            return Result.Failure(SmsConfigErrors.NotFound(request.SmsId));
        }
        applicationDbContext.SmsConfig.Remove(smsConfig);
        smsConfig.Raise(new SmsConfigDeletedDomainEvent(smsConfig.SmsId));
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}

