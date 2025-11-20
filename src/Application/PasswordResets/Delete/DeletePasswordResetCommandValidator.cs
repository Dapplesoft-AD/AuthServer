using FluentValidation;

namespace Application.PasswordResets.Delete;

internal sealed class DeletePasswordResetCommandValidator : AbstractValidator<DeletePasswordResetCommand>
{
    public DeletePasswordResetCommandValidator()
    {
        RuleFor(x => x.PrId)
            .NotEmpty().WithMessage("Password reset ID is required.");
    }
}

