using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Users.Verification.VerifyOtp;

internal class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpCommandValidator()
    {
        RuleFor(c => c.Destination)
            .NotEmpty()
            .WithMessage("Destination cannot be empty.")
            .Must(dist =>
                Regex.IsMatch(dist, @"^(?:\+?88)?01[3-9]\d{8}$") ||
                new EmailAddressAttribute().IsValid(dist)
            )
            .WithMessage("Must Input valid Email address or Bangladeshi Phone number");

        RuleFor(x => x.OtpToken)
            .NotEmpty()
            .WithMessage("OTP code is required.")
            .MinimumLength(4)
            .WithMessage("OTP code must be at least 4 characters long.")
            .MaximumLength(8)
            .WithMessage("OTP code must not exceed 8 characters long.");
    }
}
