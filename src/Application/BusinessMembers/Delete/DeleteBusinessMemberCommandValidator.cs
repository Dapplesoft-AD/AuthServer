using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.BusinessMembers.Delete;
public sealed class DeleteBusinessMemberCommandValidator
    : AbstractValidator<DeleteBusinessMemberCommand>
{
    public DeleteBusinessMemberCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
