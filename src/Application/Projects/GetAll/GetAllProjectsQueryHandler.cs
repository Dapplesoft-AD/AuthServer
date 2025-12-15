using System.Xml.Linq;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Projects.Get;
using Domain.Projects;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Projects.GetAll;
public sealed class GetAllProjectsQueryHandler
    : IQueryHandler<GetAllProjectsQuery, List<GetAllProjectsResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllProjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<Result<List<GetAllProjectsResponse>>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken)
    {
        List<GetAllProjectsResponse> applications = await _context.Projects
            .Select(a => new GetAllProjectsResponse(
                  a.Id,
                  a.Name,
                  a.ClientId,
                  a.ClientSecret,
                  a.RedirectUrl,
                  a.Status
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(applications);
    }
}
