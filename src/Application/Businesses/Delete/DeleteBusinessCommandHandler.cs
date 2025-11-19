using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Businesses.Delete;
internal sealed class DeleteBusinessCommandHandler : ICommandHandler<DeleteBusinessCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public DeleteBusinessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
    {
        Domain.Businesses.Business? business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (business is null)
        {
            return Result.Failure<Guid>($"Business with Id {request.Id} not found.");
        }

        _context.Businesses.Remove(business);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(business.Id);
    }
}
