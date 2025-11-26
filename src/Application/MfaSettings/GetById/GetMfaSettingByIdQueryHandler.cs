using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaSettings.GetById;

internal sealed class GetMfaSettingByIdQueryHandler
    : IQueryHandler<GetMfaSettingByIdQuery, MfaSettingResponse>
{
    private readonly IApplicationDbContext _context;

    public GetMfaSettingByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<MfaSettingResponse>> Handle(
        GetMfaSettingByIdQuery query,
        CancellationToken cancellationToken)
    {
        MfaSetting? mfa = await _context.MfaSettings
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (mfa is null)
        {
            return Result.Failure<MfaSettingResponse>(
                Error.NotFound("MfaSetting.NotFound", $"MfaSetting with Id {query.Id} not found.")
            );
        }

        var response = new MfaSettingResponse
        {
            Id = mfa.Id,
            UserId = mfa.UserId,
            SecretKey = mfa.SecretKey,
            BackupCodes = mfa.BackupCodes,
            Method = mfa.Method.ToString(),
            Enabled = mfa.Enabled
        };

        return response;
    }
}
