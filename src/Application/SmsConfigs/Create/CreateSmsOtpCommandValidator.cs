using FluentValidation;

namespace Application.SmsConfigs.Create;

internal sealed class CreateSmsOtpCommandValidator : AbstractValidator<CreateSmsOtpCommand>
{
    public CreateSmsOtpCommandValidator()
    {
        RuleFor(c => c.SmsToken)
            .NotEmpty().WithMessage("SMS token is required.");
    }
}
