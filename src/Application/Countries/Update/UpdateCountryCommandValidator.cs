using FluentValidation;

namespace Application.Countries.Update;


public sealed class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Country Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Country name is required.")
            .MaximumLength(150);

        RuleFor(x => x.Capital)
            .MaximumLength(150);

        RuleFor(x => x.PhoneCode)
            .NotEmpty()
            .MaximumLength(20);
    }
}
