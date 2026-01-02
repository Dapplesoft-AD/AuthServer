using FluentValidation;

namespace Application.Districts.Update;

public sealed class UpdateDistrictValidator : AbstractValidator<UpdateDistrictCommand>
{
    public UpdateDistrictValidator()
    {
        RuleFor(d => d.DistrictId)
            .NotEmpty()
            .WithMessage("DistrictId is required.");

        RuleFor(d => d.RegionId)
            .NotEmpty()
            .WithMessage("RegionId is required.");

        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("District Name is required.")
            .MaximumLength(255)
            .WithMessage("District Name cannot exceed 255 characters.");

        RuleFor(d => d.IsNew)
            .NotNull()
            .WithMessage("IsActive value is required.");
    }
}
