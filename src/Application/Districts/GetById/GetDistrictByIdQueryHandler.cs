using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Districts.Get;
using Domain.Districts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Districts.GetById;

internal sealed class GetDistrictByIdQueryHandler
    : IQueryHandler<GetDistrictByIdQuery, DistrictResponse>
{
    private readonly IApplicationDbContext _context;

    public GetDistrictByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<DistrictResponse>> Handle(
        GetDistrictByIdQuery query,
        CancellationToken cancellationToken)
    {
        District? district = await _context.Districts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (district is null)
        {
            return Result.Failure<DistrictResponse>(DistrictErrors.NotFound(query.Id));
        }

        var response = new DistrictResponse
        {
            Id = district.Id,
            RegionId = district.RegionId,
            Name = district.Name,
            IsNew = district.IsNew?? false,
        };

        return response;
    }
}
