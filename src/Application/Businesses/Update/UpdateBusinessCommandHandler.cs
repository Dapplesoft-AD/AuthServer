using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Businesses.Update;

internal sealed class UpdateBusinessCommandHandler : ICommandHandler<UpdateBusinessCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateBusinessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
    {
        Domain.Businesses.Business? business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);

        if (business is null)
        {
            return Result.Failure<Guid>(
                Error.NotFound(
                    "Business.NotFound",
                    $"Business with Id {command.Id} not found."
                )
            );
        }

        business.BusinessName = command.BusinessName;
        business.IndustryType = command.IndustryType;
        business.LogoUrl = command.LogoUrl.ToString();
        business.Status = command.Status;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(business.Id);
    }
}
