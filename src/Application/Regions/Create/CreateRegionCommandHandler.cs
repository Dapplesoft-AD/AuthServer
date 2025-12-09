using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Regions;
using SharedKernel;

namespace Application.Regions.Create;

internal sealed class CreateRegionCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateRegionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRegionCommand command, CancellationToken cancellationToken)
    {
        var region = new Region
        {
            CountryId = command.CountryId,
            Name = command.Name,
            RegionType = command.RegionType,
            IsActive = command.IsActive,
            CreatedAt = command.CreatedAt
        };

        await context.Regions.AddAsync(region, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(region.Id);
    }
}
