using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SubDistricts.Get;

public sealed class GetSubDistrictByIdQueryHandler
    : IQueryHandler<GetSubDistrictByIdQuery, SubDistrictResponse>
{
    private readonly IApplicationDbContext _context;

    public GetSubDistrictByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<SubDistrictResponse>> Handle(
        GetSubDistrictByIdQuery query,
        CancellationToken cancellationToken
    )
    {
        SubDistrictResponse? area = await _context
            .SubDistricts.Where(a => a.Id == query.Id)
            .Select(a => new SubDistrictResponse(
                a.Id,
                a.DistrictId,
                a.Name,
                a.IsNew
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (area is null)
        {
            return Result.Failure<SubDistrictResponse>("Area not found.");
        }

        return Result.Success(area);
    }
}
