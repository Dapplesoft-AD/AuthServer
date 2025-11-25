using Domain.Businesses;
using FluentValidation;

namespace Application.Businesses.Create;

public class CreateBusinessValidator : AbstractValidator<CreateBusinessCommand>
{
    public CreateBusinessValidator()
    {
        RuleFor(x => x.OwnerUserId).NotEmpty().WithMessage("OwnerUserId is required.");
        RuleFor(x => x.BusinessName).NotEmpty().MaximumLength(200).WithMessage("BusinessName is required and max 200 chars.");
        RuleFor(x => x.IndustryType).MaximumLength(100).WithMessage("IndustryType max 100 chars.");
        RuleFor(x => x.LogoUrl).MaximumLength(255).WithMessage("LogoUrl max 255 chars.");
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Status must be a valid value (Active or Inactive).");
    }
}
