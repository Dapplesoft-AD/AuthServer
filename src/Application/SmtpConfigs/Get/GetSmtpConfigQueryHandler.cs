using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SmtpConfigs.Get;

internal sealed class GetSmtpConfigQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetSmtpConfigQuery, List<SmtpConfigResponse>>
{
    public async Task<Result<List<SmtpConfigResponse>>> Handle(GetSmtpConfigQuery request, CancellationToken cancellationToken)
    {
        List<SmtpConfigResponse> smtpConfig = await context.SmtpConfig
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
            .ToListAsync(cancellationToken);
        return Result.Success(smtpConfig);
    }
}
