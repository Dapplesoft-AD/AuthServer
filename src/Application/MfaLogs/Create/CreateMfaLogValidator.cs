using FluentValidation;

namespace Application.MfaLogs.Create;

internal sealed class CreateMfaLogValidator : AbstractValidator<CreateMfaLogCommand>
{
    public CreateMfaLogValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

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
            .WithMessage("MFA status must be one of: Success (1), Failed (2).");
    }
}
