using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SmsConfigs.Get;

internal sealed class GetSmsConfigsQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetSmsConfigsQuery, List<SmsConfigResponse>>
{
    public async Task<Result<List<SmsConfigResponse>>> Handle(GetSmsConfigsQuery request, CancellationToken cancellationToken)
    {
        List<SmsConfigResponse> smsConfigs = await context.SmsConfig
            .Select(s => new SmsConfigResponse
            {
                SmsToken = s.Token,
                SmsId = s.Id
            })
            .ToListAsync(cancellationToken);
        return Result.Success(smsConfigs);
    }
}
