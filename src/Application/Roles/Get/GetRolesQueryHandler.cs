using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Roles.Get;

internal sealed class GetRolesQueryHandler
    : IQueryHandler<GetRolesQuery, List<RoleResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetRolesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<RoleResponse>>> Handle(
        GetRolesQuery query,
        CancellationToken cancellationToken)
    {
        List<RoleResponse> roles = await _context.Roles
            .Select(r => new RoleResponse(
                r.Id,
                r.RoleName,
                r.Description
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(roles);
    }
}
