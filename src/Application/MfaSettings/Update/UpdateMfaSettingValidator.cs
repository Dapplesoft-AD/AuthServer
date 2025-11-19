using FluentValidation;

namespace Application.MfaSettings.Update;

public sealed class UpdateMfaSettingValidator : AbstractValidator<UpdateMfaSettingCommand>
{
    public UpdateMfaSettingValidator()
    {
        RuleFor(m => m.MfaSettingId)
            .NotEmpty()
            .WithMessage("MfaSettingId is required.");

        RuleFor(m => m.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(m => m.Method)
            .NotEmpty()
            .WithMessage("Method is required.")
            .Must(x =>
                x.Equals("TOTP", StringComparison.OrdinalIgnoreCase) ||
                x.Equals("SMS", StringComparison.OrdinalIgnoreCase) ||
                x.Equals("EMAIL", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Method must be one of: TOTP, SMS, EMAIL.");
    }
}
