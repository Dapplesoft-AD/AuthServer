using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Customers;
using Domain.Todos;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Customers.Delete;
internal sealed class DeleteCustomerHandler(IApplicationDbContext context) : ICommandHandler<DeleteCustomerCommand>

{

    public async Task<Result> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        Customer? customer = await context.Customers
            .SingleOrDefaultAsync(c => c.Id == command.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(TodoItemErrors.NotFound(command.CustomerId));
        }

        context.Customers.Remove(customer);

        customer.Raise(new TodoItemDeletedDomainEvent(customer.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
