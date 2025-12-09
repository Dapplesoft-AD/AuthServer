using System;
using FluentValidation;

namespace Application.Localities.Create;

public class CreateLocalityValidator : AbstractValidator<CreateLocalityCommand>
{
    public CreateLocalityValidator()
    {
      
        RuleFor(x => x.CountryId)
   .NotEmpty()
   .WithMessage("CountryId is required.")
   .NotEqual(Guid.Empty)
   .WithMessage("CountryId cannot be empty GUID.");
        RuleFor(x => x.AreaId)
    .NotEmpty()
    .WithMessage("AreaId is required.")
    .NotEqual(Guid.Empty)
    .WithMessage("AreaId cannot be empty GUID.");

        RuleFor(l => l.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(255)
            .WithMessage("Name must not exceed 255 characters.");

        RuleFor(l => l.Type)
            .IsInEnum()
            .WithMessage("Invalid locality type.");
    }
}
