using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Otps.Get;

internal sealed class GetOtpsQueryHandler(
    IApplicationDbContext context) : IQueryHandler<GetOtpsQuery, List<OtpResponse>>
{
    public async Task<Result<List<OtpResponse>>> Handle(GetOtpsQuery request, CancellationToken cancellationToken)
    {
        List<OtpResponse> otps = await context.Otp
            .Select(o => new OtpResponse
            {
                OtpId = o.Id,
                Destination = o.Destination,
                IsExpired = o.IsExpired,
                OtpToken = o.OtpToken,
                Delay = o.Delay,
                ExpiresAt = o.ExpiresAt,
            })
            .ToListAsync(cancellationToken);
        return otps;
    }
}
