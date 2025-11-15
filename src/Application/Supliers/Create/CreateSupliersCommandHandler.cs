using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Customers.Create;
using Domain.Customers;
using SharedKernel;

namespace Application.Supliers.Create;
public class CreateSupliersCommandHandler : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly IApplicationDbContext context;

    public CreateSupliersCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var existingCustomer = new Customer
        {
            Name = command.Name,
            Email = command.Email,
            Address = command.Address
        };

        await context.Customers.AddAsync(existingCustomer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(existingCustomer.Id);
    }
}
