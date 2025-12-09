using FluentValidation;

namespace Application.Otps.Verify;

internal sealed class VerifyOtpVerifyCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpVerifyCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.OtpToken)
            .NotEmpty().WithMessage("OTP token is required.")
            .Length(4).WithMessage("OTP token must be 4 characters long.");
    }
}
