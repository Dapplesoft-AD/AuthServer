using FluentValidation;

namespace Application.SmsConfigs.OtpSms;

internal class SmsOtpCommandValidator : AbstractValidator<SmsOtpCommand>
{
    public SmsOtpCommandValidator()
    {
        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+\d{1,3}\d{4,14}(?:x.+)?$").WithMessage("Phone number must be in valid international format.");
    }
}
