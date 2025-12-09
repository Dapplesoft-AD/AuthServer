using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Areas;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Areas.Update;

public sealed class UpdateAreaCommandHandler
    : ICommandHandler<UpdateAreaCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        UpdateAreaCommand command,
        CancellationToken cancellationToken)
    {
        Area? area = await _context.Areas
            .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);

        if (area is null)
        {
            return Result.Failure<Guid>("Area not found.");
        }

        // Check if area name is unique within the same district (excluding current area)
        bool areaNameExists = await _context.Areas
            .AnyAsync(a =>
                a.DistrictId == command.DistrictId &&
                a.Name == command.Name &&
                a.Id != command.Id,
                cancellationToken);

        if (areaNameExists)
        {
            return Result.Failure<Guid>("Area name already exists in this district.");
        }

        // Update properties
        area.CountryId = command.CountryId;
        area.DistrictId = command.DistrictId;
        area.Name = command.Name;
        area.Type = command.Type;
        area.IsActive = command.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(area.Id);
    }
}
