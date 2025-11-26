using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaSettings.Get;

internal sealed class GetMfaSettingQueryHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : IQueryHandler<GetMfaSettingQuery, List<MfaSettingResponse>>
{
    public async Task<Result<List<MfaSettingResponse>>> Handle(
        GetMfaSettingQuery query,
        CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        List<MfaSettingResponse> settings = await context.MfaSettings
            .Where(x => x.UserId == userId)
            .Select(x => new MfaSettingResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                SecretKey = x.SecretKey,
                BackupCodes = x.BackupCodes,
                Method = x.Method.ToString(),
                Enabled = x.Enabled
            })
            .ToListAsync(cancellationToken);

        return settings;
    }
}
