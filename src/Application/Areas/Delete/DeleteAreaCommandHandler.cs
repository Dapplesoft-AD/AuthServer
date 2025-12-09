using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Areas;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Areas.Delete;

public sealed class DeleteAreaCommandHandler
    : ICommandHandler<DeleteAreaCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteAreaCommand command, CancellationToken cancellationToken)
    {
        Area? area = await _context.Areas
            .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);

        if (area is null)
        {
            return Result.Failure("Area not found.");
        }

        _context.Areas.Remove(area);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
