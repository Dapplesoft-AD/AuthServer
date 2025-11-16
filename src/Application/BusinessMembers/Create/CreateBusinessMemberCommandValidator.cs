using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.BusinessMembers.Create;
public sealed class CreateBusinessMemberCommandValidator : AbstractValidator<CreateBusinessMemberCommand>

{
    public CreateBusinessMemberCommandValidator()
    {
        RuleFor(x => x.BusinessId)
            .NotEmpty().WithMessage("BusinessId is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");
    }
}
