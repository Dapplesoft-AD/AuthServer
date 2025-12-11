using FluentValidation;

namespace Application.Otps.Create;

internal sealed class CreateOtpCommandValidator : AbstractValidator<CreateOtpCommand>
{
    public CreateOtpCommandValidator()
    {
        RuleFor(x => x)
             .Must(x => !string.IsNullOrWhiteSpace(x.Email) || !string.IsNullOrWhiteSpace(x.PhoneNumber))
             .WithMessage("Either Email or PhoneNumber must be provided.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{7,14}$") // E.164 international format
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Email) ||! string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Either Email or PhoneNumber must be provided.");
    }
}
