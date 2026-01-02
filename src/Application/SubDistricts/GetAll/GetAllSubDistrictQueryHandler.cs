using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.SubDistricts.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SubDistricts.GetAll;

public sealed class GetAllSubDistrictQueryHandler
    : IQueryHandler<GetAllSubDistrictQuery, List<SubDistrictResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllSubDistrictQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<SubDistrictResponse>>> Handle(
        GetAllSubDistrictQuery query,
        CancellationToken cancellationToken
    )
    {
        List<SubDistrictResponse> areas = await _context
            .SubDistricts.Select(a => new SubDistrictResponse(a.Id, a.DistrictId, a.Name, a.IsNew))
            .ToListAsync(cancellationToken);

        return Result.Success(areas);
    }
}
