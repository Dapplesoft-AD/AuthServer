using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Customers;
using SharedKernel;

namespace Application.Customers.Create;

public class CreateCustomerCommandHandler : ICommandHandler<CreateSupplierCommand, Guid>
{
    private readonly IApplicationDbContext context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Result<Guid>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
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
