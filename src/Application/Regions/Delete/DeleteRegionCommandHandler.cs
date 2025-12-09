using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Regions;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Regions.Delete;

internal sealed class DeleteRegionCommandHandler
    : ICommandHandler<DeleteRegionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRegionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteRegionCommand command, CancellationToken cancellationToken)
    {
        Region? region = await _context.Regions
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (region is null)
        {
            return Result.Failure(RegionErrors.NotFound(command.Id));
        }

        _context.Regions.Remove(region);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
