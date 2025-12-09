using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Countries;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Countries.Delete;

internal sealed class DeleteCountryCommandHandler : ICommandHandler<DeleteCountryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCountryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCountryCommand command, CancellationToken cancellationToken)
    {
        Country? country = await _context.Countries
            .SingleOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (country is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Country.NotFound",
                    $"Country with Id {command.Id} not found."
                )
            );
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
