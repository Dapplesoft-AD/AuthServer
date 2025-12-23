using FluentValidation;

namespace Application.Tokens.Delete;

internal sealed class DeleteTokenCommandValidator : AbstractValidator<DeleteTokenCommand>
{
    public DeleteTokenCommandValidator()
    {
        RuleFor(x => x.TokenId)
            .NotEmpty().WithMessage("Token ID is required.");
    }
}
