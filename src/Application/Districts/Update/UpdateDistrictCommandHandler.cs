using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Districts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Districts.Update;

internal sealed class UpdateDistrictCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<UpdateDistrictCommand>
{
    public async Task<Result> Handle(UpdateDistrictCommand command, CancellationToken cancellationToken)
    {
        District? district = await context.Districts
            .SingleOrDefaultAsync(d => d.Id == command.DistrictId, cancellationToken);

        if (district is null)
        {
            return Result.Failure(Error.NotFound(
                "District.NotFound",
                $"District with Id {command.DistrictId} not found."));
        }

        // Update fields
        district.RegionId = command.RegionId;
        district.Name = command.Name;
        district.IsNew = command.IsNew;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
