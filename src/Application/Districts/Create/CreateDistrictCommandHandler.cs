using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Districts;
using SharedKernel;

namespace Application.Districts.Create;

internal sealed class CreateDistrictCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateDistrictCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDistrictCommand command, CancellationToken cancellationToken)
    {
        var district = new District
        {
            CountryId = command.CountryId,
            RegionId = command.RegionId,
            Name = command.Name,
            IsActive = command.IsActive,
            CreatedAt = command.CreatedAt
        };

        await context.Districts.AddAsync(district, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(district.Id);
    }
}
