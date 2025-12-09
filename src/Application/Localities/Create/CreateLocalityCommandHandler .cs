using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Localities;
using SharedKernel;

namespace Application.Localities.Create;

public sealed class CreateLocalityCommandHandler
    : ICommandHandler<CreateLocalityCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateLocalityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        CreateLocalityCommand command,
        CancellationToken cancellationToken)
    {
        var locality = new Locality
        {
            CountryId = command.CountryId,
            AreaId = command.AreaId,
            Name = command.Name,
            Type = command.Type,
            IsActive = command.IsActive
        };

        await _context.Localities.AddAsync(locality, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(locality.Id);
    }
}

