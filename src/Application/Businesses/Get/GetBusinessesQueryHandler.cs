using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Businesses.Get;

internal sealed class GetBusinessesQueryHandler : IQueryHandler<GetBusinessesQuery, List<GetBusinessResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public GetBusinessesQueryHandler(IApplicationDbContext context, IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<List<GetBusinessResponse>>> Handle(
        GetBusinessesQuery request,
        CancellationToken cancellationToken)
    {
        Guid userId = _userContext.UserId;

        List<GetBusinessResponse> businesses = await _context.Businesses
            .Where(b => b.OwnerUserId == userId)
            .Select(b => new GetBusinessResponse
            {
                Id = b.Id,
                OwnerUserId = b.OwnerUserId,
                BusinessName = b.BusinessName,
                IndustryType = b.IndustryType,
                LogoUrl = b.LogoUrl,
                Status = b.Status,
                CreatedAt = b.CreatedAt
            })
            .ToListAsync(cancellationToken);
        return Result.Success(businesses);
    }
}
