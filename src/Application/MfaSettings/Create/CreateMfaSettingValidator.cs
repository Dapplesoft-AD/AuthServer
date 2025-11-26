using FluentValidation;
using Domain.MfaSettings;


namespace Application.MfaSettings.Create;


internal sealed class CreateMfaSettingValidator : AbstractValidator<CreateMfaSettingCommand>
{
    public CreateMfaSettingValidator()
    {
        RuleFor(m => m.UserId)
        .NotEmpty()
        .WithMessage("UserId is required.");


        RuleFor(m => m.SecretKey)
        .NotEmpty()
        .WithMessage("Secret key is required.")
        .MaximumLength(255);


        
        RuleFor(m => m.Method)
        .IsInEnum()
        .WithMessage("MFA method must be one of: TOTP, SMS, or EMAIL.");


        RuleFor(m => m.BackupCodes)
        .NotEmpty()
        .WithMessage("Backup codes are required.");


        RuleFor(m => m.Enabled)
        .NotNull()
        .WithMessage("Enabled status must be specified.");
    }
}
