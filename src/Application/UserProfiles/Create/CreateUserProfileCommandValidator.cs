using FluentValidation;

namespace Application.UserProfiles.Create;

internal sealed class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>
{
    public CreateUserProfileCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(255);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100);

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100);

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.")
            .MaximumLength(100);

        RuleFor(x => x.ProfileImageUrl)
            .NotEmpty().WithMessage("Profile image URL is required.")
            .MaximumLength(255)
            .Must(BeAValidUrl).WithMessage("Profile image URL must be a valid URL.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeAValidBirthDate).WithMessage("Date of birth must be a valid past date.");
    }

    private bool BeAValidBirthDate(DateOnly date)
    {
        return date < DateOnly.FromDateTime(DateTime.Today);    // Must be in the past and at least 10 years ago
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }

}
