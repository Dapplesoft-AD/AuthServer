using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.BusinessMembers.Update;

internal sealed class UpdateBusinessMemberCommandHandler : ICommandHandler<UpdateBusinessMemberCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateBusinessMemberCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(UpdateBusinessMemberCommand command, CancellationToken cancellationToken)
    {
        Domain.BusinessMembers.BusinessMember? member = await _context.BusinessMembers
            .FirstOrDefaultAsync(bm => bm.Id == command.Id, cancellationToken);

        if (member == null)
        {
            return Result.Failure<Guid>(
                Error.NotFound("BusinessMember.NotFound", "The specified business member does not exist.")
            );
        }

        member.BusinessId = command.BusinessId;
        member.UserId = command.UserId;
        member.RoleId = command.RoleId;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(member.Id);
    }
}
