using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Permissions.Get;
using Domain.Applications;
using Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Applications.Update;

public sealed class UpdateApplicationCommandHandler
    : ICommandHandler<UpdateApplicationCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateApplicationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(UpdateApplicationCommand command, CancellationToken cancellationToken)
    {
        Applicationapply? application = await _context.Applications
            .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);
        if (application is null)
        {
            return Result.Failure<Guid>("application not found.");
        }

        // Check if ClientId is unique (excluding current application)
        bool clientIdExists = await _context.Applications
            .AnyAsync(a => a.ClientId == command.ClientId && a.Id != command.Id, cancellationToken);

        if (clientIdExists)
        {
            return Result.Failure<Guid>("ClientId already exists.");
        }

        application.Name = command.Name;
        application.ClientId = command.ClientId;
        application.ClientSecret = command.ClientSecret;
        application.RedirectUri = command.RedirectUri.ToString();
        application.ApiBaseUrl = command.ApiBaseUrl.ToString();
        application.Status = command.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(application.Id);
    }
}
