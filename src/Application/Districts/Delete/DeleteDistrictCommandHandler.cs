using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Districts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Districts.Delete;

internal sealed class DeleteDistrictCommandHandler
    : ICommandHandler<DeleteDistrictCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDistrictCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteDistrictCommand command, CancellationToken cancellationToken)
    {
        District? district = await _context.Districts
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (district is null)
        {
            return Result.Failure(DistrictErrors.NotFound(command.Id));
        }

        _context.Districts.Remove(district);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
