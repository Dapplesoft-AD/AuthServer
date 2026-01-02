using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Districts.Get;

internal sealed class GetDistrictQueryHandler(
    IApplicationDbContext context)
    : IQueryHandler<GetDistrictQuery, List<DistrictResponse>>
{
    public async Task<Result<List<DistrictResponse>>> Handle(
        GetDistrictQuery query,
        CancellationToken cancellationToken)
    {
        List<DistrictResponse> districts = await context.Districts
            .Select(x => new DistrictResponse
            {
                Id = x.Id,
                RegionId = x.RegionId,
                Name = x.Name,
                IsNew = x.IsNew,
            })
            .ToListAsync(cancellationToken);

        return districts;
    }
}
