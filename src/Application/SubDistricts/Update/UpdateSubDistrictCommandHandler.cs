using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SubDistricts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SubDistricts.Update;

public sealed class UpdateSubDistrictCommandHandler
    : ICommandHandler<UpdateSubDistrictCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateSubDistrictCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        UpdateSubDistrictCommand command,
        CancellationToken cancellationToken
    )
    {
        SubDistrict? area = await _context.SubDistricts.FirstOrDefaultAsync(
            a => a.Id == command.Id,
            cancellationToken
        );

        if (area is null)
        {
            return Result.Failure<Guid>("Area not found.");
        }

        // Check if area name is unique within the same district (excluding current area)
        bool areaNameExists = await _context.SubDistricts.AnyAsync(
            a => a.DistrictId == command.DistrictId && a.Name == command.Name && a.Id != command.Id,
            cancellationToken
        );

        if (areaNameExists)
        {
            return Result.Failure<Guid>("Area name already exists in this district.");
        }

        // Update properties
        area.DistrictId = command.DistrictId;
        area.Name = command.Name;
        area.IsNew = command.IsNew;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(area.Id);
    }
}
