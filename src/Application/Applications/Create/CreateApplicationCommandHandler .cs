using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Applications;
using SharedKernel;

namespace Application.Applications.Create;

public sealed class CreateApplicationCommandHandler
    : ICommandHandler<CreateApplicationCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateApplicationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateApplicationCommand command, CancellationToken cancellationToken)
    {
        var application = new Applicationapply
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            ClientId = command.ClientId,
            ClientSecret = command.ClientSecret,
            RedirectUri = command.RedirectUri.ToString(), // Convert Uri to string
            ApiBaseUrl = command.ApiBaseUrl.ToString(),   // Convert Uri to string
            Status = command.Status
        };

        await _context.Applications.AddAsync(application, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(application.Id);
    }
}
