using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SmsConfigs;
using SharedKernel;

namespace Application.SmsConfigs.Create;

internal class CreateSmsOtpCommandHandler(
    IApplicationDbContext applicationDbContext) : ICommandHandler<CreateSmsOtpCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSmsOtpCommand request, CancellationToken cancellationToken)
    {
        var smsConfig = new SmsConfig
        {
            Id = Guid.NewGuid(),
            ProviderName = request.ProviderName,
            ProviderUrl = request.ProviderUrl,
            ApiUrl = request.ApiUrl,
            Token = request.SmsToken
        };
        smsConfig.Raise(new SmsConfigCreatedDomainEvent(smsConfig.Id));
        applicationDbContext.SmsConfig.Add(smsConfig);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return smsConfig.Id;
    }
}
