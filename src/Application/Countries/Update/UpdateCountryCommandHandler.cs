using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Countries;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Countries.Update;

internal sealed class UpdateCountryCommandHandler : ICommandHandler<UpdateCountryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCountryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCountryCommand command, CancellationToken cancellationToken)
    {
        Country? country = await _context.Countries
            .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (country is null)
        {
            return Result.Failure(
                Error.NotFound(
                    "Country.NotFound",
                    $"Country with Id {command.Id} not found."
                )
            );
        }

        country.Name = command.Name;
        country.Capital = command.Capital;
        country.PhoneCode = command.PhoneCode;
        country.IsActive = command.IsActive;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
