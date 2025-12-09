using System;
using FluentValidation;

namespace Application.Areas.Update;

public class UpdateAreaValidator : AbstractValidator<UpdateAreaCommand>
{
    public UpdateAreaValidator()
    {
       
        RuleFor(x => x.CountryId)
   .NotEmpty()
   .WithMessage("CountryId is required.")
   .NotEqual(Guid.Empty)
   .WithMessage("CountryId cannot be empty GUID.");
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

        RuleFor(a => a.Type)
            .IsInEnum()
            .WithMessage("Invalid area type.");

        RuleFor(a => a.IsActive)
            .NotNull()
            .WithMessage("IsActive is required.");
    }
}
