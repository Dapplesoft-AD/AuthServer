using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Applications.Delete;
public sealed class DeleteApplicationCommandHandler
    : ICommandHandler<DeleteApplicationCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteApplicationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteApplicationCommand command, CancellationToken cancellationToken)
    {
        Domain.Applications.Applicationapply? application = await _context.Applications
            .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken) ?? throw new InvalidOperationException("Application not found.");

        _context.Applications.Remove(application);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
