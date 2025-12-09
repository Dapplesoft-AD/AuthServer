using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Countries;
using SharedKernel;

namespace Application.Countries.Create;

internal sealed class CreateCountryCommandHandler
    : ICommandHandler<CreateCountryCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCountryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        CreateCountryCommand request,
        CancellationToken cancellationToken)
    {
        var country = new Country
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Capital = request.Capital,
            PhoneCode = request.PhoneCode,
            IsActive = request.IsActive
        };

        await _context.Countries.AddAsync(country, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(country.Id);
    }
}
