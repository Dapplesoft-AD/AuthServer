using FluentValidation;

namespace Application.EmailVerification.Create;

public class CreateEmailVerificationCommandValidator : AbstractValidator<CreateEmailVerificationCommand>
{
    public CreateEmailVerificationCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.")
            .MaximumLength(255).WithMessage("Token must not exceed 255 characters.");
        RuleFor(x => x.ExpiresAt)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
        RuleFor(x => x.VerifiedAt)
            .GreaterThan(DateTime.UtcNow).WithMessage("Verification date must be in the future.");
    }
}
