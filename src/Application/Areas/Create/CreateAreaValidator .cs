using System;
using FluentValidation;

namespace Application.Areas.Create;

    public class CreateAreaValidator : AbstractValidator<CreateAreaCommand>
    {
        public CreateAreaValidator()
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

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(255)
                .WithMessage("Name must not exceed 255 characters.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage(
                    "Invalid area type. Valid values: 1=Upazila, 2=City, 3=Thana, 4=Municipality, 5=Township."
                );
        }
    }

