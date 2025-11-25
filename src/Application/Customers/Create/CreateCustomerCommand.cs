using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Customers.Create;

public sealed class CreateCustomerCommand : ICommand<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
