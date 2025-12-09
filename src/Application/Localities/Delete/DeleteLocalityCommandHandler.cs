using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Localities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Localities.Delete;

public sealed class DeleteLocalityCommandHandler
    : ICommandHandler<DeleteLocalityCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLocalityCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteLocalityCommand command, CancellationToken cancellationToken)
    {
        Locality? locality = await _context.Localities
            .FirstOrDefaultAsync(l => l.Id == command.Id, cancellationToken);

        if (locality is null)
        {
            return Result.Failure("Locality not found.");
        }

        _context.Localities.Remove(locality);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
