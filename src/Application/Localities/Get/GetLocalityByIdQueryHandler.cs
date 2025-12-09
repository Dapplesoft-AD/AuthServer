using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Localities.Get;

public sealed class GetLocalityByIdQueryHandler
    : IQueryHandler<GetLocalityByIdQuery, LocalityResponse>
{
    private readonly IApplicationDbContext _context;

    public GetLocalityByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<LocalityResponse>> Handle(
        GetLocalityByIdQuery query,
        CancellationToken cancellationToken)
    {
        LocalityResponse? locality = await _context.Localities
            .Where(l => l.Id == query.Id)
            .Select(l => new LocalityResponse(
                l.Id,
                l.CountryId,
                l.AreaId,
                l.Name,
                (int)l.Type,
                l.Type.ToString(),
                l.IsActive
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (locality is null)
        {
            return Result.Failure<LocalityResponse>("Locality not found.");
        }

        return Result.Success(locality);
    }
}
