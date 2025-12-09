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
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new DistrictResponse
            {
                Id = x.Id,
                CountryId = x.CountryId,
                RegionId = x.RegionId,
                Name = x.Name,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToListAsync(cancellationToken);

        return districts;
    }
}
