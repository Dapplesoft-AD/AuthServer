using FluentValidation;

namespace Application.Regions.Update;

public sealed class UpdateRegionValidator : AbstractValidator<UpdateRegionCommand>
{
    public UpdateRegionValidator()
    {
        RuleFor(r => r.RegionId)
            .NotEmpty()
            .WithMessage("RegionId is required.");

        RuleFor(r => r.CountryId)
            .NotEmpty()
            .WithMessage("CountryId is required.");

        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Region Name is required.")
            .MaximumLength(255)
            .WithMessage("Region Name cannot exceed 255 characters.");

        RuleFor(r => r.RegionType)
            .NotEmpty()
            .WithMessage("RegionType is required.")
            .MaximumLength(100)
            .WithMessage("RegionType cannot exceed 100 characters.");

        RuleFor(r => r.IsActive)
            .NotNull()
            .WithMessage("IsActive value is required.");
    }
}
