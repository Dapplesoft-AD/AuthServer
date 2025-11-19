using FluentValidation;

namespace Application.EmailVerification.Delete;

internal sealed class DeleteEmailVerificationCommandValidator : AbstractValidator<DeleteEmailVerificationCommand>
{
    public DeleteEmailVerificationCommandValidator()
    {
               RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Email Verification ID is required.");
    }
}
