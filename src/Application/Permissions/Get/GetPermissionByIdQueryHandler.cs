using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Permissions.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Permissions.GetById;

public sealed class GetPermissionByIdQueryHandler
    : IQueryHandler<GetPermissionByIdQuery, PermissionResponse>
{
    private readonly IApplicationDbContext _context;

    public GetPermissionByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PermissionResponse>> Handle(GetPermissionByIdQuery query, CancellationToken cancellationToken)
    {
        PermissionResponse permission = await _context.Permissions
            .Where(p => p.Id == query.Id)
            .Select(p => new PermissionResponse(p.Id, p.Code, p.Description))
            .FirstOrDefaultAsync(cancellationToken);
        if (permission is null)
        {
            return Result.Failure<PermissionResponse>("Permission not found.");
        }

        return Result.Success(permission);
    }
}
