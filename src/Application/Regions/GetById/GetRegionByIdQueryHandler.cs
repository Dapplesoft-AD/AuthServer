using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Regions.Get;
using Domain.Regions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Regions.GetById;

internal sealed class GetRegionByIdQueryHandler
    : IQueryHandler<GetRegionByIdQuery, RegionResponse>
{
    private readonly IApplicationDbContext _context;

    public GetRegionByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<RegionResponse>> Handle(
        GetRegionByIdQuery query,
        CancellationToken cancellationToken)
    {
        Region? region = await _context.Regions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (region is null)
        {
            return Result.Failure<RegionResponse>(RegionErrors.NotFound(query.Id));
        }

        var response = new RegionResponse
        {
            Id = region.Id,
            CountryId = region.CountryId,
            Name = region.Name,
            RegionType = region.RegionType,
            IsActive = region.IsActive,
            CreatedAt = region.CreatedAt,
            UpdatedAt = region.UpdatedAt
        };

        return response;
    }
}
