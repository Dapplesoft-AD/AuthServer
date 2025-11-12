using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Customers.Delete;
public sealed class DeleteCustomerCommand : ICommand
{
    public Guid CustomerId { get; init; }
    public DeleteCustomerCommand(Guid _customerId)
    {
        CustomerId = _customerId;
    }
}
