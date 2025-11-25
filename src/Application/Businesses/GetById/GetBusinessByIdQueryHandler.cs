using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Businesses.GetById;

internal sealed class GetBusinessByIdQueryHandler : IQueryHandler<GetBusinessByIdQuery, GetBusinessByIdResponse>
{
    private readonly IApplicationDbContext _context;
    public GetBusinessByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetBusinessByIdResponse>> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
    {
        GetBusinessByIdResponse? business = await _context.Businesses
            .Where(b => b.Id == request.Id)
            .Select(b => new GetBusinessByIdResponse
            {
                Id = b.Id,
                OwnerUserId = b.OwnerUserId,
                BusinessName = b.BusinessName,
                IndustryType = b.IndustryType,
                LogoUrl = b.LogoUrl,
                Status = b.Status,
                CreatedAt = b.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (business is null)
        {
            return Result.Failure<GetBusinessByIdResponse>(
                Error.NotFound("Business.NotFound", $"Business with Id {request.Id} not found.")
            );
        }

        return Result.Success(business);
    }
}
