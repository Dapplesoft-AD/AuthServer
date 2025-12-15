using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Projects.Get;
using Application.RolePermissions.Get;
using Domain.Projects;
using Domain.RolePermissions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Projects.Delete;
public sealed class DeleteProjectCommandHandler
    : ICommandHandler<DeleteProjectCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        Project? application = await _context.Projects
            .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);
        if (application is null)
        {
            return Result.Failure<GetProjectResponse>("Application not found.");
        }
        _context.Projects.Remove(application);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
