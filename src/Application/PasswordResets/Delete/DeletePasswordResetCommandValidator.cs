using Application.Abstractions.Messaging;
using FluentValidation;

namespace Application.PasswordResets.Delete;

internal sealed class DeletePasswordResetCommandValidator : AbstractValidator<DeletePasswordResetCommand>
{
    public DeletePasswordResetCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Password reset ID is required.");
    }
}

