using FluentValidation;

namespace Application.Otps.Create;

internal sealed class CreateOtpCommandValidator : AbstractValidator<CreateOtpCommand>
{
    public CreateOtpCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
