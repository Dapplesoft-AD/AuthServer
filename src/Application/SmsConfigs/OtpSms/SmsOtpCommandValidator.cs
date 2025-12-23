using FluentValidation;

namespace Application.SmsConfigs.OtpSms;

internal class SmsOtpCommandValidator : AbstractValidator<SmsOtpCommand>
{
    public SmsOtpCommandValidator()
    {
        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{7,14}$").WithMessage("Phone number must be in valid international format.");
    }
}
