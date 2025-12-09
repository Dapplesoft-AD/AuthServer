using Application.Abstractions.Authentication;
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
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new RegionResponse
            {
                Id = x.Id,
                CountryId = x.CountryId,
                Name = x.Name,
                RegionType = x.RegionType,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return regions;
    }
}
