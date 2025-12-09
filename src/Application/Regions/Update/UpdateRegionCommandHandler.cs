using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Regions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Regions.Update;

internal sealed class UpdateRegionCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<UpdateRegionCommand>
{
    public async Task<Result> Handle(UpdateRegionCommand command, CancellationToken cancellationToken)
    {
        Region? region = await context.Regions
            .SingleOrDefaultAsync(r => r.Id == command.RegionId, cancellationToken);

        if (region is null)
        {
            return Result.Failure(Error.NotFound(
                "Region.NotFound",
                $"Region with Id {command.RegionId} not found."));
        }

        // Update fields
        region.CountryId = command.CountryId;
        region.Name = command.Name;
        region.RegionType = command.RegionType;
        region.IsActive = command.IsActive;
        region.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
