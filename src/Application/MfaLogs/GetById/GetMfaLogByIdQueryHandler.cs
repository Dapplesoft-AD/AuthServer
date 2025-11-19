using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MfaLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MfaLogs.GetById;

internal sealed class GetMfaLogByIdQueryHandler
    : IQueryHandler<GetMfaLogByIdQuery, MfaLogResponse>
{
    private readonly IApplicationDbContext _context;

    public GetMfaLogByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<MfaLogResponse>> Handle(
        GetMfaLogByIdQuery query,
        CancellationToken cancellationToken)
    {
        MfaLog? mfaLog = await _context.MfaLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (mfaLog is null)
        {
            return Result.Failure<MfaLogResponse>(MfaLogErrors.NotFound(query.Id));
        }

        var response = new MfaLogResponse
        {
            Id = mfaLog.Id,
            UserId = mfaLog.UserId,
            LoginTime = mfaLog.LoginTime,
            IpAddress = mfaLog.IpAddress,
            Device = mfaLog.Device,
            Status = mfaLog.Status,
            CreatedAt = mfaLog.CreatedAt,
            UpdatedAt = mfaLog.UpdatedAt
        };

        return response;
    }
}
