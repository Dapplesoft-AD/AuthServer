using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using Domain.Areas;
using SharedKernel;

namespace Application.Areas.Create;

public sealed class CreateAreaCommandHandler
    : ICommandHandler<CreateAreaCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        CreateAreaCommand command,
        CancellationToken cancellationToken)
    {
        var area = new Area
        {
            CountryId = command.CountryId,
            DistrictId = command.DistrictId,
            Name = command.Name,
            Type = command.Type,
            IsActive = command.IsActive
        };

        await _context.Areas.AddAsync(area, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(area.Id);
    }
}
