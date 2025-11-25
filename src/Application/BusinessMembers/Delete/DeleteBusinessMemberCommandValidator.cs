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
