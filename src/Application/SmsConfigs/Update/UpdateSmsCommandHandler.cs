using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SmsConfigs;
using SharedKernel;

namespace Application.SmsConfigs.Update;

internal sealed class UpdateSmsCommandHandler(
    IApplicationDbContext applicationDbContext) : ICommandHandler<UpdateSmsCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateSmsCommand command, CancellationToken cancellationToken)
    {
        SmsConfig? smsConfig = await applicationDbContext.SmsConfig
            .FindAsync([command.SmsId], cancellationToken);
        if (smsConfig is null)
        {
            return Result.Failure<Guid>(SmsConfigErrors.NotFound(command.SmsId));
        }
        smsConfig.SmsToken = command.SmsToken;
        applicationDbContext.SmsConfig.Update(smsConfig);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(smsConfig.SmsId);
    }
}
