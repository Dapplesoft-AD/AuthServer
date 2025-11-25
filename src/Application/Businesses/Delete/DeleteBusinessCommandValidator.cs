using FluentValidation;

namespace Application.Businesses.Delete;
public sealed class DeleteBusinessCommandValidator : AbstractValidator<DeleteBusinessCommand>
{
    public DeleteBusinessCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Business Id is required.");
    }
}
