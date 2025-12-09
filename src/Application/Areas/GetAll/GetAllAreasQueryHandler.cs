using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Areas.Get;
using Application.Areas.GetAl;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Areas.GetAll;

public sealed class GetAllAreasQueryHandler
    : IQueryHandler<GetAllAreasQuery, List<AreaResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllAreasQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<AreaResponse>>> Handle(
        GetAllAreasQuery query,
        CancellationToken cancellationToken)
    {
        List<AreaResponse> areas = await _context.Areas
            .Select(a => new AreaResponse(
                a.Id,
                a.CountryId,
                a.DistrictId,
                a.Name,
                (int)a.Type,
                a.Type.ToString(),
                a.IsActive
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(areas);
    }
}
