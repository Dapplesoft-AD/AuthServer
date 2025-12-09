using FluentValidation;

namespace Application.Regions.Create;

public sealed class CreateRegionValidator : AbstractValidator<CreateRegionCommand>
{
    public CreateRegionValidator()
    {
        RuleFor(r => r.CountryId)
            .NotEmpty()
            .WithMessage("CountryId is required.");

        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(200)
            .WithMessage("Name cannot exceed 200 characters.");

        RuleFor(r => r.RegionType)
            .NotEmpty()
            .WithMessage("RegionType is required.")
            .MaximumLength(100)
            .WithMessage("RegionType cannot exceed 100 characters.");

        RuleFor(r => r.CreatedAt)
            .NotEmpty()
            .WithMessage("CreatedAt is required.");
    }
}
