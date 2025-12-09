using FluentValidation;

namespace Application.SmtpConfigs.Create;

internal sealed class CreateSmtpConfigCommandValidator : AbstractValidator<CreateSmtpConfigCommand>
{
    public CreateSmtpConfigCommandValidator()
    {
        RuleFor(c => c.Host)
            .NotEmpty().WithMessage("Host is required.");
        RuleFor(c => c.Port)
            .GreaterThan(0).WithMessage("Port must be greater than 0.");
        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("Username is required.");
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required.");
        RuleFor(c => c.SenderEmail)
            .NotEmpty().WithMessage("Sender email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
