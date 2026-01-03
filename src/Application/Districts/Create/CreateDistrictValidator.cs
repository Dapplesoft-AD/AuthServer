using FluentValidation;

namespace Application.Districts.Create;

public sealed class CreateDistrictValidator : AbstractValidator<CreateDistrictCommand>
{
    public CreateDistrictValidator()
    {

        RuleFor(d => d.RegionId)
            .NotEmpty()
            .WithMessage("RegionId is required.");

        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(200)
            .WithMessage("Name cannot exceed 200 characters.");
    }
}
