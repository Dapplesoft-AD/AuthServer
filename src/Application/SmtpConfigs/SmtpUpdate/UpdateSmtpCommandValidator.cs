using FluentValidation;

namespace Application.SmtpConfigs.SmtpUpdate;

internal sealed class UpdateSmtpCommandValidator : AbstractValidator<UpdateSmtpCommand>
{
    public UpdateSmtpCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(16).WithMessage("Password must be at least 16 characters long.");
        RuleFor(x => x.SenderEmail)
            .NotEmpty().WithMessage("Sender email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
