using Application.Todos.Create;
using FluentValidation;

namespace Application.EmailVerification.Create;

public class CreateEmailVerificationCommandValidator : AbstractValidator<CreateEmailVerificationCommand>
{
    public CreateEmailVerificationCommandValidator()
    {		RuleFor(x => x.User_Id)
            .NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.")
            .MaximumLength(256).WithMessage("Token must not exceed 256 characters.");
        RuleFor(x => x.Expires_at)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
        RuleFor(x => x.Verified_at)
            .GreaterThan(DateTime.UtcNow).WithMessage("Verification date must be in the future.");
    }
}
