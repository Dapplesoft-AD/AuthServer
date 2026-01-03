using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain;
using Domain.SubDistricts;
using SharedKernel;

namespace Application.SubDistricts.Create;

public sealed class CreateAreaCommandHandler : ICommandHandler<CreateAreaCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(
        CreateAreaCommand command,
        CancellationToken cancellationToken
    )
    {
        var subDistrict = new SubDistrict
        {
            DistrictId = command.DistrictId,
            Name = command.Name,
            IsNew = command.IsNew,
        };

        await _context.SubDistricts.AddAsync(subDistrict, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(subDistrict.Id);
    }
}
