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

<<<<<<< HEAD
public class CreateCustomerCommandHandler : ICommandHandler<CreateSupplierCommand, Guid>
=======
public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Guid>
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79
{
    private readonly IApplicationDbContext context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

<<<<<<< HEAD
    public async Task<Result<Guid>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
=======
    public async Task<Result<Guid>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79
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
