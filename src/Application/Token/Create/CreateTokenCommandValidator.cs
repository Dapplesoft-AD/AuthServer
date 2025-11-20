using FluentValidation;

namespace Application.Token.Create;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.AppId)
            .NotEmpty().WithMessage("App ID is required.")
            .NotEqual(Guid.Empty);
        RuleFor(x => x.Accesstoken)
            .NotEmpty().WithMessage("Access token is required.")
            .MaximumLength(500).WithMessage("Access token cannot exceed 500 characters.");
        RuleFor(x => x.Refreshtoken)
            .NotEmpty().WithMessage("Refresh token is required.")
            .MaximumLength(500).WithMessage("Refresh token cannot exceed 500 characters.");
        RuleFor(x => x.IssuedAt)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Issued at cannot be in the future.");
    }
}
