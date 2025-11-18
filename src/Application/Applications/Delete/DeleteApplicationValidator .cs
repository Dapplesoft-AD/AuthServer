using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Applications.Delete;
public class DeleteApplicationValidator : AbstractValidator<DeleteApplicationCommand>
{
    public DeleteApplicationValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty()
            .WithMessage("Application ID is required.");
    }
}
