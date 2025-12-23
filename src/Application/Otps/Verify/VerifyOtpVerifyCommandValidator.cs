using FluentValidation;

namespace Application.Otps.Verify;

internal sealed class VerifyOtpVerifyCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpVerifyCommandValidator()
    {
        RuleFor(x => x)
            .Must(x =>
                !string.IsNullOrWhiteSpace(x.Email) ||
                !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Either Email or PhoneNumber must be provided.");

        RuleFor(x => x.Email)
           .EmailAddress()
           .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{7,14}$") // E.164 international format
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.OtpToken)
            .NotEmpty().WithMessage("OTP token is required.")
            .Length(4).WithMessage("OTP token must be 4 characters long.");
    }
}
