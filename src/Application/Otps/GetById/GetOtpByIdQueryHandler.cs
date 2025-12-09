using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Otps.GetById;

internal sealed class GetOtpByIdQueryHandler(
    IApplicationDbContext applicationDbContext) : IQueryHandler<GetOtpByIdQuery, OtpResponse>
{
    public async Task<Result<OtpResponse>> Handle(GetOtpByIdQuery request, CancellationToken cancellationToken)
    {

        OtpResponse otps = await applicationDbContext.Otp
            .Where(o => o.OtpId == request.OtpId)
            .Select(o => new OtpResponse
            {
                OtpId = o.OtpId,
                Email = o.Email,
                IsExpired = o.IsExpired,
                OtpToken = o.OtpToken,
                Delay = o.Delay,
                CreatedAt = o.CreatedAt
            })
            .SingleOrDefaultAsync(cancellationToken);
        if (otps is null)
        {
            return Result.Failure<OtpResponse>("OTP not found.");
        }
        return Result.Success(otps);
    }
}
