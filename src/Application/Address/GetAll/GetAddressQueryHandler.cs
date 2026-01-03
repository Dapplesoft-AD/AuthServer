using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Address.GetAll;

internal sealed class GetAddressQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetAddressQuery, List<GetAddressQueryResponse>>
{
    public async Task<Result<List<GetAddressQueryResponse>>> Handle(
        GetAddressQuery query,
        CancellationToken cancellationToken
    )
    {
        List<GetAddressQueryResponse> addresses = await context
            .Countries.AsNoTracking()
            .Select(country => new GetAddressQueryResponse
            {
                Country = new CountryDto
                {
                    Id = country.Id,
                    Name = country.Name,
                    Regions = context
                        .Regions.Where(region => region.CountryId == country.Id)
                        .AsNoTracking()
                        .Select(region => new RegionDto
                        {
                            Id = region.Id,
                            Name = region.Name,
                            Districts = context
                                .Districts.Where(district => district.RegionId == region.Id)
                                .AsNoTracking()
                                .Select(district => new DistrictDto
                                {
                                    Id = district.Id,
                                    Name = district.Name,
                                    SubDistricts = context
                                        .SubDistricts
                                        .Where(subDistrict => subDistrict.DistrictId == district.Id)
                                        .AsNoTracking()
                                        .Select(subDistrict => new SubDistrictDto
                                        {
                                            Id = subDistrict.Id,
                                            Name = subDistrict.Name,
                                        })
                                        .ToList()
                                })
                                .ToList()
                        })
                        .ToList()
                }
            })
            .ToListAsync(cancellationToken);

        return Result<List<GetAddressQueryResponse>>.Success(addresses);
    }
}
