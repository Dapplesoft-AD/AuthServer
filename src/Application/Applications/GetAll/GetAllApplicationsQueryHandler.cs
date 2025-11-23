using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Applications.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Applications.GetAll;
public sealed class GetAllApplicationsQueryHandler
    : IQueryHandler<GetAllApplicationsQuery, List<ApplicationResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllApplicationsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<ApplicationResponse>>> Handle(GetAllApplicationsQuery query, CancellationToken cancellationToken)
    {
        List<ApplicationResponse> applications = await _context.Applications
            .Select(a => new ApplicationResponse(
                a.Id,
                a.Name,
                a.ClientId,
                a.ClientSecret,
                new Uri(a.RedirectUri),   
                new Uri(a.ApiBaseUrl),
                (int)a.Status,
                a.Status.ToString()
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(applications);
    }
}
