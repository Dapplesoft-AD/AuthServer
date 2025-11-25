using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.BusinessMembers.GetById;

internal sealed class GetBusinessMemberByIdQueryHandler : IQueryHandler<GetBusinessMemberByIdQuery, BusinessMemberResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBusinessMemberByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<BusinessMemberResponse>> Handle(GetBusinessMemberByIdQuery query, CancellationToken cancellationToken)
    {
        BusinessMemberResponse? member = await _context.BusinessMembers
            .Where(bm => bm.Id == query.Id)
            .Select(bm => new BusinessMemberResponse
            {
                Id = bm.Id,
                BusinessId = bm.BusinessId,
                UserId = bm.UserId,
                RoleId = bm.RoleId,
                JoinedAt = bm.JoinedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (member == null)
        {
            return Result.Failure<BusinessMemberResponse>(
                SharedKernel.Error.NotFound("BusinessMember.NotFound", "The specified business member does not exist.")
            );
        }

        return Result.Success(member);
    }
}
