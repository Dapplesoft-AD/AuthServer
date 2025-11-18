using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Applications.Get;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Applications.GetById;

public sealed class GetApplicationByIdQueryHandler
    : IQueryHandler<GetApplicationByIdQuery, ApplicationResponse>
{
    private readonly IApplicationDbContext _context;

    public GetApplicationByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ApplicationResponse>> Handle(GetApplicationByIdQuery query, CancellationToken cancellationToken)
    {
        ApplicationResponse? application = await _context.Applications
            .Where(a => a.Id == query.Id)
            .Select(a => new ApplicationResponse(
                a.Id,
                a.Name,
                a.ClientId,
                a.ClientSecret,
                 new Uri(a.RedirectUri),   // ✅ convert string → Uri
            new Uri(a.ApiBaseUrl),      // Already string from database
                (int)a.Status,
                a.Status.ToString()
            ))
            .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException("Application not found.");

        return Result.Success(application);
    }
}
