using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Localities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Localities.Update;

public sealed class UpdateLocalityCommandHandler
    : ICommandHandler<UpdateLocalityCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateLocalityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        UpdateLocalityCommand command,
        CancellationToken cancellationToken)
    {
        Locality? locality = await _context.Localities
            .FirstOrDefaultAsync(l => l.Id == command.Id, cancellationToken);

        if (locality is null)
        {
            return Result.Failure<Guid>("Locality not found.");
        }

        // Check if locality name is unique within the same area (excluding current locality)
        bool localityNameExists = await _context.Localities
            .AnyAsync(l =>
                l.AreaId == command.AreaId &&
                l.Name == command.Name &&
                l.Id != command.Id,
                cancellationToken);

        if (localityNameExists)
        {
            return Result.Failure<Guid>("Locality name already exists in this area.");
        }

        // Update properties
        locality.CountryId = command.CountryId;
        locality.AreaId = command.AreaId;
        locality.Name = command.Name;
        locality.Type = command.Type;
        locality.IsActive = command.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(locality.Id);
    }
}
