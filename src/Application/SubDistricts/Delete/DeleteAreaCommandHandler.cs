using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.SubDistricts;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.SubDistricts.Delete;

public sealed class DeleteAreaCommandHandler : ICommandHandler<DeleteAreaCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAreaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteAreaCommand command, CancellationToken cancellationToken)
    {
        SubDistrict? area = await _context.SubDistricts.FirstOrDefaultAsync(
            a => a.Id == command.Id,
            cancellationToken
        );

        if (area is null)
        {
            return Result.Failure("Area not found.");
        }

        _context.SubDistricts.Remove(area);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
