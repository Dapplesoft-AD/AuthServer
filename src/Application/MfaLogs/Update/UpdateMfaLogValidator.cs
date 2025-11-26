using FluentValidation;

namespace Application.MfaLogs.Update;

public sealed class UpdateMfaLogValidator : AbstractValidator<UpdateMfaLogCommand>
{
    public UpdateMfaLogValidator()
    {
        RuleFor(x => x.MfaLogId)
            .NotEmpty()
            .WithMessage("MfaLogId is required.");

        RuleFor(x => x.LoginTime)
            .NotEmpty()
            .WithMessage("LoginTime is required.");

        RuleFor(x => x.IpAddress)
            .NotEmpty()
            .WithMessage("IpAddress is required.")
            .MaximumLength(50)
            .WithMessage("IpAddress cannot exceed 50 characters.");

        RuleFor(x => x.Device)
            .NotEmpty()
            .WithMessage("Device is required.")
            .MaximumLength(100)
            .WithMessage("Device cannot exceed 100 characters.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid MFA status value.");
    }
}
