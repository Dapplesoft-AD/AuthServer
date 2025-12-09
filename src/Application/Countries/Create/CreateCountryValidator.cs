using FluentValidation;

namespace Application.Countries.Create;

public sealed class CreateCountryValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Capital)
            .MaximumLength(150);

        RuleFor(x => x.PhoneCode)
            .NotEmpty()
            .MaximumLength(20);
    }
}
