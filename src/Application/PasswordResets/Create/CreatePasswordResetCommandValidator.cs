using Application.Application.Create;
using FluentValidation;

namespace Application.PasswordResets.Create;

public class CreatePasswordResetCommandValidator : AbstractValidator<CreatePasswordResetCommand>
{
    public CreatePasswordResetCommandValidator()
    {          RuleFor(x => x.User_Id)
            .NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.")
            .MaximumLength(256).WithMessage("Token must not exceed 256 characters.");
        RuleFor(x => x.Expires_at)
            .GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
    }
}
