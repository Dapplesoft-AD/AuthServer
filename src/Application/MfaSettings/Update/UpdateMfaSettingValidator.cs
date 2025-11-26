using FluentValidation;
using Domain.MfaSettings;

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
            .IsInEnum()
            .WithMessage("Method must be one of: TOTP, SMS, EMAIL.");
    }
}
