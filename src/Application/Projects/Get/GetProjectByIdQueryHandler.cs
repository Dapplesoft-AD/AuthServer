using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Projects.Get;
using Application.RolePermissions.Get;
using Domain.RolePermissions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Projects.Get;

public sealed class GetProjectByIdQueryHandler
    : IQueryHandler<GetProjectByIdQuery, GetProjectResponse>
{
    private readonly IApplicationDbContext _context;

    public GetProjectByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetProjectResponse>> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
    {
        GetProjectResponse? application = await _context.Projects
            .Where(a => a.Id == query.Id)
            .Select(a => new GetProjectResponse(
                a.Id,
                a.Name,
                a.ClientId,
                a.ClientSecret,
                a.Domain,
                a.RedirectUrl,
                a.Status
            ))
            .FirstOrDefaultAsync(cancellationToken) ;
        
        if (application is null)
        {
            return Result.Failure<GetProjectResponse>("Role permission not found.");
        }

        return Result.Success(application);
    }
}
