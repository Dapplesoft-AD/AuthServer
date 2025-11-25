using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.BusinessMembers;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.BusinessMembers.Create;

public sealed class CreateBusinessMemberCommandHandler : ICommandHandler<CreateBusinessMemberCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateBusinessMemberCommandHandler(IApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(CreateBusinessMemberCommand command, CancellationToken cancellationToken)
    {
        bool businessExists = await _context.Businesses
     .AnyAsync(b => b.Id == command.BusinessId, cancellationToken);

        if (!businessExists)
        {
            return Result.Failure<Guid>(
                Error.NotFound("Business.NotFound", "The specified business does not exist."));
        }

        bool userExists = await _context.Users
            .AnyAsync(u => u.Id == command.UserId, cancellationToken);

        if (!userExists)
        {
            return Result.Failure<Guid>(
                Error.NotFound("User.NotFound", "The specified user does not exist."));
        }

        bool roleExists = await _context.Roles
            .AnyAsync(r => r.Id == command.RoleId, cancellationToken);

        if (!roleExists)
        {
            return Result.Failure<Guid>(
                Error.NotFound("Role.NotFound", "The specified role does not exist."));
        }

        bool alreadyMember = await _context.BusinessMembers
            .AnyAsync(m => m.BusinessId == command.BusinessId
                        && m.UserId == command.UserId, cancellationToken);

        if (alreadyMember)
        {
            return Result.Failure<Guid>(
                Error.Conflict("BusinessMember.Exists", "This user is already a member of the business."));
        }

        var member = new BusinessMember
        {
            Id = Guid.NewGuid(),
            BusinessId = command.BusinessId,
            UserId = command.UserId,
            RoleId = command.RoleId,
            JoinedAt = _dateTimeProvider.UtcNow
        };

        await _context.BusinessMembers.AddAsync(member, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(member.Id);
    }

}
