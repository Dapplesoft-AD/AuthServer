using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Permissions.Get;
using Domain.Projects;
using Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Projects.Update;

public sealed class UpdateProjectCommandHandler
    : ICommandHandler<UpdateProjectCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
    {
        Project? application = await _context.Projects
            .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);
        if (application is null)
        {
            return Result.Failure<Guid>("application not found.");
        }

        // Check if ClientId is unique (excluding current application)
        bool clientIdExists = await _context.Projects
            .AnyAsync(a => a.ClientId == command.ClientId && a.Id != command.Id, cancellationToken);

        if (clientIdExists)
        {
            return Result.Failure<Guid>("ClientId already exists.");
        }

        application.Name = command.Name;
        application.ClientId = command.ClientId;
        application.ClientSecret = command.ClientSecret;
        application.Domain = command.Domain;
        application.RedirectUrl = command.RedirectUrl;
        application.Status = command.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(application.Id);
    }
}
