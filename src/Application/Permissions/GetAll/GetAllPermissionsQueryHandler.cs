using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Permissions.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Permissions.GetAll;

public sealed class GetAllPermissionsQueryHandler
    : IQueryHandler<GetAllPermissionsQuery, List<PermissionResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllPermissionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<PermissionResponse>>> Handle(GetAllPermissionsQuery query, CancellationToken cancellationToken)
    {
        List<PermissionResponse> permissions = await _context.Permissions
            .Select(p => new PermissionResponse(p.Id, p.Code, p.Description))
            .ToListAsync(cancellationToken);

        return Result.Success(permissions);
    }
}
