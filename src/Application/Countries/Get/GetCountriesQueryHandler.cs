using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Countries.Get;

internal sealed class GetCountriesQueryHandler
    : IQueryHandler<GetCountriesQuery, List<GetCountryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetCountriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<GetCountryResponse>>> Handle(
        GetCountriesQuery request,
        CancellationToken cancellationToken)
    {
        List<GetCountryResponse> countries = await _context.Countries
            .AsNoTracking()
            .Select(b => new GetCountryResponse
            {
                Id = b.Id,
                Name = b.Name,
                Capital = b.Capital,
                PhoneCode = b.PhoneCode,
                IsActive = b.IsNew
            })
            .ToListAsync(cancellationToken);
        return Result.Success(countries);
    }
}
