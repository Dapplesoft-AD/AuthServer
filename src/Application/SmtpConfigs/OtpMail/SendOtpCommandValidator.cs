using FluentValidation;

namespace Application.SmtpConfigs.OtpMail;

internal class SendOtpCommandValidator : AbstractValidator<SendOtpCommand>
{
    public SendOtpCommandValidator()
    {
        RuleFor(x => x.RecipientEmail)
            .NotEmpty().WithMessage("Recipient email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
