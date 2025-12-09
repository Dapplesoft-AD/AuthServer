using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Countries.GetById;

internal sealed class GetCountryByIdQueryHandler : IQueryHandler<GetCountryByIdQuery, GetCountryByIdResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCountryByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetCountryByIdResponse>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        GetCountryByIdResponse country = await _context.Countries
            .AsNoTracking()
            .Where(b => b.Id == request.Id)
            .Select(b => new GetCountryByIdResponse
            {
                Id = b.Id,
                Name = b.Name,
                Capital = b.Capital,
                PhoneCode = b.PhoneCode,
                IsActive = b.IsActive
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (country is null)
        {
            return Result.Failure<GetCountryByIdResponse>(
                Error.NotFound(
                    "Country.NotFound",
                    $"Country with Id {request.Id} not found."
                )
            );
        }

        return Result.Success(country);
    }
}
