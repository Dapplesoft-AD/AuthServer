using FluentValidation;

namespace Application.SubDistricts.Update;

public class UpdateSubDistrictValidator : AbstractValidator<UpdateSubDistrictCommand>
{
    public UpdateSubDistrictValidator()
    {
        RuleFor(x => x.DistrictId)
            .NotEmpty()
            .WithMessage("ID is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("DistrictId cannot be empty GUID.");

        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(255)
            .WithMessage("Name must not exceed 255 characters.");

        RuleFor(a => a.IsNew).NotNull().WithMessage("IsActive is required.");
    }
}
