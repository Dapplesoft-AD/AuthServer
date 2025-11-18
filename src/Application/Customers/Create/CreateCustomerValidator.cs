using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Customers.Create;

<<<<<<< HEAD
public class CreateCustomerValidator: AbstractValidator<CreateSupplierCommand>
=======
public class CreateCustomerValidator: AbstractValidator<CreateCustomerCommand>
>>>>>>> 8be9632798b68d2f9b5ec678c438c63cb1b8eb79
{
    public CreateCustomerValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(c => c.Address)
            .NotEmpty()
            .WithMessage("Address is required.");
    }
}
