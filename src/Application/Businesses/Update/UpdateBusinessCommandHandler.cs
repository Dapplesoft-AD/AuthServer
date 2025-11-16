using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Businesses;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Businesses.Update;
internal sealed class UpdateBusinessCommandHandler : ICommandHandler<UpdateBusinessCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateBusinessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        Domain.Businesses.Business? business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (business is null)
        {
            return Result.Failure<Guid>($"Business with Id {request.Id} not found.");
        }

        // Update fields
        business.BusinessName = request.BusinessName;
        business.IndustryType = request.IndustryType;
        business.LogoUrl = request.LogoUrl;
        business.Status = Status.defaultValue;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(business.Id);
    }
}
