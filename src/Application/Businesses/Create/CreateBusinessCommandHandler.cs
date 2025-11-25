using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Businesses;
using SharedKernel;

namespace Application.Businesses.Create;

public class CreateBusinessCommandHandler : ICommandHandler<CreateBusinessCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateBusinessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
    {

        var business = new Business
        {
            Id = Guid.NewGuid(),
            OwnerUserId = request.OwnerUserId,
            BusinessName = request.BusinessName,
            IndustryType = request.IndustryType,
            LogoUrl = request.LogoUrl,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };

        _context.Businesses.Add(business);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(business.Id);
    }
}
