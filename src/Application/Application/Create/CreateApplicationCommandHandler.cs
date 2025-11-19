using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Application;
using Application.Application.Create;
using SharedKernel;

namespace Application.Application.Create;

public class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateApplicationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = new Applications
        {
            Name = request.Name,
            Client_id = request.Client_id,
            Client_secret = request.Client_secret,
            Redirect_url = request.Redirect_url,
            Api_base_url = request.Api_base_url,
            Application_status = request.Application_status
        };
        _context.Applications.Add(application);
        await _context.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(application.Id);
    }
}
