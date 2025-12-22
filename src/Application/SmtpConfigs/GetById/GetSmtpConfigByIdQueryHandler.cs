using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SmtpConfigs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SmtpConfigs.GetById;

internal sealed class GetSmtpConfigByIdQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetSmtpConfigByIdQuery, SmtpConfigResponse>
{
    public async Task<Result<SmtpConfigResponse>> Handle(GetSmtpConfigByIdQuery request, CancellationToken cancellationToken)
    {
        SmtpConfigResponse smtpConfig = await context.SmtpConfig
            .Where(s => s.SmtpId == request.SmtpId)
            .Select(s => new SmtpConfigResponse
            {
                SmtpId = s.SmtpId,
                Host = s.Host,
                Port = s.Port,
                Username = s.Username,
                Password = s.Password,
                EnableSsl = s.EnableSsl,
                SenderEmail = s.SenderEmail
            })
            .SingleOrDefaultAsync(cancellationToken);
        if (smtpConfig is null)
        {
            return Result.Failure<SmtpConfigResponse>(SmtpConfigErrors.NotFound(request.SmtpId));
        }
        return smtpConfig;
    }
}
