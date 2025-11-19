using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Businesses.Delete;
public sealed class DeleteBusinessCommandValidator : AbstractValidator<DeleteBusinessCommand>
{
    public DeleteBusinessCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Business Id is required.");
    }
}
