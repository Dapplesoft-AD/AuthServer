using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.BusinessMembers.Get;

internal sealed class GetAllBusinessMembersQueryHandler : IQueryHandler<GetBusinessMembersQuery, List<BusinessMemberResponse>>
{
    private readonly IApplicationDbContext _context;
    public GetAllBusinessMembersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<BusinessMemberResponse>>> Handle(GetBusinessMembersQuery query, CancellationToken cancellationToken)
    {
        List<BusinessMemberResponse> members = await _context.BusinessMembers
            .Select(bm => new BusinessMemberResponse
            {
                Id = bm.Id,
                BusinessId = bm.BusinessId,
                UserId = bm.UserId,
                RoleId = bm.RoleId,
                JoinedAt = bm.JoinedAt
            })
            .ToListAsync(cancellationToken);
             return Result.Success(members);
    }
}


