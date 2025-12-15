using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SmsConfigs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SmsConfigs.GetById;

internal sealed class GetSmsConfigByIdQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetSmsConfigByIdQuery, SmsConfigResponse>
{
    public async Task<Result<SmsConfigResponse>> Handle(GetSmsConfigByIdQuery request, CancellationToken cancellationToken)
    {
        SmsConfigResponse smsConfig = await context.SmsConfig
            .Where(s => s.SmsId == request.SmsId)
            .Select(s => new SmsConfigResponse
            {
                SmsToken = s.SmsToken,
                SmsId = s.SmsId
            })
            .SingleOrDefaultAsync(cancellationToken);
        if (smsConfig is null)
        {
            return Result.Failure<SmsConfigResponse>(SmsConfigErrors.NotFound(request.SmsId));
        }
        return smsConfig;
    }
}
