using FluentValidation;

namespace Application.Countries.Delete;

public sealed class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
{
    public DeleteCountryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Country Id is required.");
    }
}
