using FluentValidation;

namespace Application.PasswordResets.Create;

public class CreatePasswordResetCommandValidator : AbstractValidator<CreatePasswordResetCommand>
{
    public CreatePasswordResetCommandValidator()
    {
        RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.")
            .MaximumLength(256).WithMessage("Token must not exceed 256 characters.");
        RuleFor(x => x.ExpiresAt)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
        RuleFor(x => x.Used)
            .NotNull().WithMessage("Used status must be specified.");
    }
}
