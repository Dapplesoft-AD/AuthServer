using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Customers.Create;

<<<<<<< HEAD
public sealed class CreateSupplierCommand: ICommand<Guid>
=======
public sealed class CreateCustomerCommand: ICommand<Guid>
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
