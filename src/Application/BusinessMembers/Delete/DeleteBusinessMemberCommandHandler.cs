using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.BusinessMembers.Delete;

public sealed class DeleteBusinessMemberCommandHandler
    : ICommandHandler<DeleteBusinessMemberCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public DeleteBusinessMemberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(    
        DeleteBusinessMemberCommand command,
        CancellationToken cancellationToken)
    {
        Domain.BusinessMembers.BusinessMember? member = await _context.BusinessMembers
            .FirstOrDefaultAsync(m => m.Id == command.Id, cancellationToken);

        if (member is null)
        {
            return Result.Failure<Guid>(
                Error.NotFound("BusinessMember.NotFound", "Business member not found"));
        }

        _context.BusinessMembers.Remove(member);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(command.Id);
    }
}
