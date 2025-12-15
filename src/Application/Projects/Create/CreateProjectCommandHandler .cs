using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Projects;
using SharedKernel;

namespace Application.Projects.Create;

public sealed class CreateProjectCommandHandler
    : ICommandHandler<CreateProjectCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            ClientId = command.ClientId,
            ClientSecret = command.ClientSecret,
            RedirectUrl = command.RedirectUrl,
            Status = command.Status
        };

        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(project.Id);
    }
}
