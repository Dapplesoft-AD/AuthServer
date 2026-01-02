using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Regions.Get;

internal sealed class GetRegionQueryHandler(
    IApplicationDbContext context)
    : IQueryHandler<GetRegionQuery, List<RegionResponse>>
{
    public async Task<Result<List<RegionResponse>>> Handle(
        GetRegionQuery query,
        CancellationToken cancellationToken)
    {
    

        List<RegionResponse> regions = await context.Regions
            .Select(x => new RegionResponse
            {
                Id = x.Id,
                CountryId = x.CountryId,
                Name = x.Name,
                IsNew = x.IsNew
            })
            .ToListAsync(cancellationToken);

        return regions;
    }
}
