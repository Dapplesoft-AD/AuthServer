using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SmtpConfigs;
using SharedKernel;

namespace Application.SmtpConfigs.Create;

internal class CreateSmtpConfigCommandHandler(
    IApplicationDbContext applicationDbContext) : ICommandHandler<CreateSmtpConfigCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSmtpConfigCommand request, CancellationToken cancellationToken)
    {
        var smtpConfig = new SmtpConfig
        {
            Host = request.Host,
            Port = request.Port,
            Username = request.Username,
            Password = request.Password,
            SenderEmail = request.SenderEmail
        };
        applicationDbContext.SmtpConfig.Add(smtpConfig);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return smtpConfig.SmtpId;
    }
}
