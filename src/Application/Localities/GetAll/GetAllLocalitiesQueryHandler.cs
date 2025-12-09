using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Localities.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Localities.GetAll;

public sealed class GetAllLocalitiesQueryHandler
    : IQueryHandler<GetAllLocalitiesQuery, List<LocalityResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllLocalitiesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<LocalityResponse>>> Handle(
        GetAllLocalitiesQuery query,
        CancellationToken cancellationToken)
    {
        List<LocalityResponse> localities = await _context.Localities
            .Select(l => new LocalityResponse(
                l.Id,
                l.CountryId,
                l.AreaId,
                l.Name,
                (int)l.Type,
                l.Type.ToString(),
                l.IsActive
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(localities);
    }
}
