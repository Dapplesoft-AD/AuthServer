using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Areas.Get;

public sealed class GetAreaByIdQueryHandler
    : IQueryHandler<GetAreaByIdQuery, AreaResponse>
{
    private readonly IApplicationDbContext _context;

    public GetAreaByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<AreaResponse>> Handle(
        GetAreaByIdQuery query,
        CancellationToken cancellationToken)
    {
        AreaResponse? area = await _context.Areas.Where(a => a.Id == query.Id)
            .Select(a => new AreaResponse(
                a.Id,
                a.CountryId,
                a.DistrictId,
                a.Name,
                (int)a.Type,
                a.Type.ToString(),
                a.IsActive
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (area is null)
        {
            return Result.Failure<AreaResponse>("Area not found.");
        }

        return Result.Success(area);
    }
}
